using Yzh.Bosai.Net.ScoreManager.Shared;

namespace Yzh.Bosai.Net.ScoreManager.Domain.Service
{
    /// <summary>
    /// 用户积分服务
    /// </summary>
    public interface ICustomerScoreService
    {
        /// <summary>
        /// 尝试添加用户积分
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerScore"></param>
        /// <returns></returns>
        bool TryAdd(long customerId, CustomerScore customerScore);

        /// <summary>
        /// 尝试更新用户积分
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="scoreChange"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        bool TryUpdate(long customerId, double scoreChange, out CustomerScore existing);

        /// <summary>
        /// 尝试移除用户积分
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerScore"></param>
        /// <returns></returns>
        bool TryRemove(long customerId, out CustomerScore customerScore);

        /// <summary>
        /// 尝试获取用户积分
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerScore"></param>
        /// <returns></returns>
        bool TryGetValue(long customerId, out CustomerScore customerScore);
    }
}
