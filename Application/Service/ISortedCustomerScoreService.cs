using Yzh.Bosai.Net.ScoreManager.Application.Dto;

namespace Yzh.Bosai.Net.ScoreManager.Application.Service
{
    /// <summary>
    /// 排序后的用户积分服务
    /// </summary>
    public interface ISortedCustomerScoreService
    {
        /// <summary>
        /// 更新排序后的分数
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="score"></param>
        void UpdateSortedScores(long customerId, double score);

        /// <summary>
        /// 获取排名区间内的用户
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        IEnumerable<CustomerScoreRankResponse> GetCustomersByRank(int start, int end);

        /// <summary>
        /// 获取指定用户的上下邻居
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        List<CustomerScoreRankResponse> GetCustomerRankWithNeighbors(long customerId, int high, int low);
    }
}
