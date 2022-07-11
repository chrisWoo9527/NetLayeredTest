using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Transactions;

namespace 自启动过滤筛选器
{
    public class TransactionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 判断action上面是否有 NotTransactionalAttribute 这个特性 如果有就不走了
            bool hasNotTransactionalAttribute = false;

            // 判断是不是mvc/api 的action描述
            if (context.ActionDescriptor is ControllerActionDescriptor)
            {
                // 转换下
                ControllerActionDescriptor actionDes = (ControllerActionDescriptor)context.ActionDescriptor;
                // 获得是否又注解 根据反射来的
                hasNotTransactionalAttribute = actionDes.MethodInfo.IsDefined(typeof(NoTransactionScopeAttribute));
            }

            // 有这个注解就直接走了
            if (hasNotTransactionalAttribute)
            {
                await next();
                return;
            }

            using var transactionScope = new TransactionScope();
            ActionExecutedContext actionExecutedContext = await next();

            // 无异常就提交
            if (actionExecutedContext.Exception != null)
            {
                transactionScope.Complete();
            }


        }
    }
}
