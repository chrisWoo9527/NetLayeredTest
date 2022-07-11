namespace 自启动过滤筛选器
{
    // 使用在方法中

    [AttributeUsage(AttributeTargets.Method)]
    public class NoTransactionScopeAttribute : Attribute
    {
    }
}
