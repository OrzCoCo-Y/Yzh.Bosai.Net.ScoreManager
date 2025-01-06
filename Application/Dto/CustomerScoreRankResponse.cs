namespace Yzh.Bosai.Net.ScoreManager.Application.Dto
{
    /// <summary>
    /// 用户积分排名响应
    /// </summary>
    public class CustomerScoreRankResponse
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// 用户积分
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// 排名
        /// </summary>
        public int Rank { get; set; }
    }
}
