using Yzh.Bosai.Net.ScoreManager.Shared;

namespace Yzh.Bosai.Net.ScoreManager.Application.Service
{
    /// <summary>
    /// 积分管理核心服务
    /// </summary>
    public interface IScoreManagerCoreService
    {
        /// <summary>
        /// 更新用户积分
        /// update the score of a customer
        /// </summary>
        /// <param name="customerId">用户Id</param>
        /// <param name="score">分数</param>
        Task UpdateScore(long customerId, double score);

        /// <summary>
        /// 获取指定用户的分数
        /// get the rank of a specific customer
        /// </summary>
        /// <param name="customerId">用户名</param>
        /// <param name="highNeighbors"></param>
        /// <param name="lowNeighbors"></param>
        /// <returns></returns>
        CustomerScore GetCustomerById(long customerId, int? highNeighbors, int? lowNeighbors);
    }
}
