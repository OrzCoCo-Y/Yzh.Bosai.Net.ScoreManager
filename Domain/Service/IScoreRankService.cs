using Yzh.Bosai.Net.ScoreManager.Shared;

namespace Yzh.Bosai.Net.ScoreManager.Domain.Service
{
    /// <summary>
    /// 用户积分排名服务
    /// </summary>
    public interface IScoreRankService
    {

        /// <summary>
        /// 更新或添加用户积分排名
        /// </summary>
        /// <param name="customerScore"></param>
        void AddOrUpdate(CustomerScore customerScore);

        /// <summary>
        /// 移除用户积分排名
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        bool Remove(long customerId);

        /// <summary>
        /// 获取指定位置排名的用户积分
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        IEnumerable<CustomerScore> GetRange(int start, int end);
    }
}
