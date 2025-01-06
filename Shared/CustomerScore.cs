namespace Yzh.Bosai.Net.ScoreManager.Shared
{
    /// <summary>
    /// 用户积分
    /// </summary>
    public class CustomerScore
    {
        public long CustomerId { get; set; }
        public double Score { get; set; } = 0;
    }
}
