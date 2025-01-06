namespace Yzh.Bosai.Net.ScoreManager.Application.Dto
{
    /// <summary>
    /// 用户积分变更Dto
    /// </summary>
    public class CustomerScoreChangedDto
    {
        public long CustomerId { get; }
        public double Score { get; }

        public CustomerScoreChangedDto(long customerId, double score)
        {
            CustomerId = customerId;
            Score = score;
        }
    }
}
