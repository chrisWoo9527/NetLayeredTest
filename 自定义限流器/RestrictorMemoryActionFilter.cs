using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace 自定义限流器
{
    public class RestrictorMemoryActionFilter : IAsyncActionFilter
    {
        private readonly IMemoryCache _memoryCache;

        public RestrictorMemoryActionFilter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 获取发动的IP地址为key
            string ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            string cacheKey = $"LastVisitTick_{ipAddress}";
            long? lastTisk = _memoryCache.Get<long?>(cacheKey);

            if (lastTisk == null || Environment.TickCount64 - lastTisk > 1000)
            {
                await next();
                // 将启动时间放进去
                _memoryCache.Set(cacheKey, Environment.TickCount64, TimeSpan.FromSeconds(10));
            }
            else
            {
                ObjectResult result = new ObjectResult("当前访问过于频繁") { StatusCode = 429 };
                context.Result = result;
            }
        }
    }
}
