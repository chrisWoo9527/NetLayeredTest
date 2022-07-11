using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetLayeredTest
{
    public class MyExcepiton : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment _Env;

        public MyExcepiton(IWebHostEnvironment env)
        {
            _Env = env;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            string msg = "";

            if (_Env.IsDevelopment())
            {
                msg = context.Exception.ToString();
            }

            else
            {
                msg = "出现全局异常请检查日志";
                await System.IO.File.WriteAllTextAsync(@"D:\DeskTop\FileUpdate\Errorlog.txt", context.Exception.ToString());
            }

            ObjectResult obj = new ObjectResult(new { code = 200, message = msg });
            context.Result = obj;
            context.ExceptionHandled = true;   // true 代表这个异常已经被处理过了
 


        }
    }
}
