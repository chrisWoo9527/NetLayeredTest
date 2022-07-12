namespace 自定义限流器.Restrictor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DistributeTimesRestrictorAttribute : Attribute
    {
        public int Times { get; set; }

        public DistributeTimesRestrictorAttribute(int times)
        {
            Times = times;
        }
    }
}
