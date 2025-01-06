namespace Yzh.Bosai.Net.ScoreManager.Application.Dto
{
    /// <summary>
    /// 用户积分变更Dto
    /// </summary>
    public class CustomerScoreChangedDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long CustomerId { get; }

        /// <summary>
        /// 积分
        /// </summary>
        public double Score { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="score"></param>
        public CustomerScoreChangedDto(long customerId, double score)
        {
            CustomerId = customerId;
            Score = score;
        }
    }
}
