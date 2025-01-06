using System.Collections.Concurrent;
using Yzh.Bosai.Net.ScoreManager.Domain.Service;
using Yzh.Bosai.Net.ScoreManager.Shared;

namespace Yzh.Bosai.Net.ScoreManager.Infrastructure.Services
{
    /// <summary>
    /// 客户评分
    /// </summary>
    public class CustomerScoresService : ICustomerScoreService
    {

        /// <summary>
        /// 用于存储客户评分信息
        /// key - 客户ID
        /// value - 客户评分
        /// </summary>
        private readonly ConcurrentDictionary<long, CustomerScore> _scores = new ConcurrentDictionary<long, CustomerScore>();

#if DEBUG
        private static readonly Lazy<ConcurrentDictionary<long, CustomerScore>> _lazyScores;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static CustomerScoresService()
        {
            var initialScores = new List<CustomerScore>
            {
                new() { CustomerId = 798, Score = 98.5 },
                new() { CustomerId = 123, Score = 176.3 },
                new() { CustomerId = 666, Score = 82228.4 },
                new() { CustomerId = 4396, Score = -92.1 },
                new() { CustomerId = 888, Score = 3379.0 }
            };

            _lazyScores = new Lazy<ConcurrentDictionary<long, CustomerScore>>(() =>
            {
                var scores = new ConcurrentDictionary<long, CustomerScore>();
                foreach (var customerScore in initialScores)
                {
                    scores.TryAdd(customerScore.CustomerId, customerScore);
                }
                return scores;
            });
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerScoresService()
        {
            _scores = _lazyScores.Value;
        }

#endif

        /// <summary>
        /// 尝试添加用户积分
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerScore"></param>
        /// <returns></returns>
        public bool TryAdd(long customerId, CustomerScore customerScore)
        {
            return _scores.TryAdd(customerId, customerScore);
        }


        /// <summary>
        /// 尝试更新用户积分
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="scoreChange"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        public bool TryUpdate(long customerId, double scoreChange, out CustomerScore existing)
        {
            if (_scores.TryGetValue(customerId, out var current))
            {
                var updated = new CustomerScore { CustomerId = customerId, Score = current.Score + scoreChange };
                existing = updated;
                return _scores.TryUpdate(customerId, updated, current);
            }
            else
            {
                existing = null;
                return false;
            }
        }

        /// <summary>
        /// 尝试移除用户积分
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerScore"></param>
        /// <returns></returns>
        public bool TryRemove(long customerId, out CustomerScore customerScore)
        {
            return _scores.TryRemove(customerId, out customerScore);
        }

        /// <summary>
        /// 尝试获取用户积分
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerScore"></param>
        /// <returns></returns>
        public bool TryGetValue(long customerId, out CustomerScore customerScore)
        {
            return _scores.TryGetValue(customerId, out customerScore);
        }
    }
}
