using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace 自定义限流器
{
    public class DistributeTimesRestrictorActionFilter : IAsyncActionFilter
    {
        private readonly IDistributedCache _distributedCache;

        public DistributeTimesRestrictorActionFilter(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string key = DistributeTimesRestrictorOptions.Key;
            string? Times = await _distributedCache.GetStringAsync(key);

            if (Times == null || Convert.ToInt32(Times) < DistributeTimesRestrictorOptions.Times)
            {
                await next();
                DateTime DateTime1 = DateTime.Now;
                //第二天的0点00分00秒
                DateTime DateTime2 = DateTime.Now.AddDays(1).Date;//把2个时间转成TimeSpan,方便计算
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //时间比较，得出差值
                TimeSpan ts = ts1.Subtract(ts2).Duration();//结果
                DistributedCacheEntryOptions distributedCacheEntryOptions = new DistributedCacheEntryOptions();
                distributedCacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(ts.Seconds);  // 数据只存储当天的
                await _distributedCache.SetStringAsync(key, Times == null ? "1" : (Convert.ToInt32(Times) + 1).ToString());
            }
            else
            {
                ObjectResult objectResult = new ObjectResult($"当前访问次数不可大于{DistributeTimesRestrictorOptions.Times}次") { StatusCode = 429 };
                context.Result = objectResult;
            }

        }
    }
}
