using Yzh.Bosai.Net.ScoreManager.Domain.Service;
using Yzh.Bosai.Net.ScoreManager.Shared;

namespace Yzh.Bosai.Net.ScoreManager.Infrastructure.Services
{
    /// <summary>
    /// 排序的用户积分
    /// </summary>
    public class ScoreRankService : IScoreRankService
    {

        /// <summary>
        /// 比较器
        /// </summary>
        private static readonly CustomerScoreComparer customerScoreComparer = new();

        /// <summary>
        ///  排好序的用户积分，有序集合
        /// </summary>
        private readonly SortedSet<CustomerScore> _rankedCustomerScores = new(customerScoreComparer);

        /// <summary>
        /// 读写锁
        /// </summary>
        private readonly ReaderWriterLockSlim _lock = new();


#if DEBUG

        private static readonly Lazy<SortedSet<CustomerScore>> _lazyRankedCustomerScores;

        // 静态构造函数，确保只初始化一次
        static ScoreRankService()
        {
            var initialScores = new List<CustomerScore>
            {
                new() { CustomerId = 798, Score = 98.5 },
                new() { CustomerId = 123, Score = 176.3 },
                new() { CustomerId = 666, Score = 82228.4 },
                new() { CustomerId = 4396, Score = -92.1 },
                new() { CustomerId = 888, Score = 3379.0 }
            };

            _lazyRankedCustomerScores = new Lazy<SortedSet<CustomerScore>>(() =>
            {

                var rankedCustomerScores = new SortedSet<CustomerScore>(customerScoreComparer);
                foreach (var customerScore in initialScores)
                {
                    rankedCustomerScores.Add(customerScore);
                }
                return rankedCustomerScores;
            });

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ScoreRankService()
        {
            _rankedCustomerScores = _lazyRankedCustomerScores.Value;
        }

#endif

        /// <summary>
        /// 添加或更新用户积分
        /// </summary>
        /// <param name="customerScore"></param>
        public void AddOrUpdate(CustomerScore customerScore)
        {
            _lock.EnterWriteLock();
            try
            {
                // 尝试找到已存在的 CustomerScore
                var existingScore = _rankedCustomerScores.FirstOrDefault(cs => customerScore.CustomerId.Equals(cs.CustomerId));

                if (existingScore != null)
                {
                    // 如果找到了，则移除旧条目并添加新条目
                    _rankedCustomerScores.Remove(existingScore);
                    _rankedCustomerScores.Add(customerScore);
                }
                else
                {
                    // 如果没有找到，则直接添加新条目
                    _rankedCustomerScores.Add(customerScore);
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool Remove(long customerId)
        {
            _lock.EnterWriteLock();
            try
            {
                return _rankedCustomerScores.RemoveWhere(score => score.CustomerId == customerId) > 0;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 获取区间内的用户积分
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<CustomerScore> GetRange(int start, int end)
        {
            _lock.EnterReadLock();
            try
            {
                return _rankedCustomerScores.Skip(start - 1).Take(end - start + 1).ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }

}

