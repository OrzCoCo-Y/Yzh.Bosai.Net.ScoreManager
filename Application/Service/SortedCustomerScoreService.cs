using Yzh.Bosai.Net.ScoreManager.Application.Dto;
using Yzh.Bosai.Net.ScoreManager.Domain.Service;

namespace Yzh.Bosai.Net.ScoreManager.Application.Service
{
    /// <summary>
    /// 排序后的用户积分服务
    /// </summary>
    public class SortedCustomerScoreService : ISortedCustomerScoreService
    {
        private readonly IScoreRankService _sortedScores;
        private readonly ICustomerScoreService _scores;

        public virtual bool IgnoreNegative => true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scoreRankService"></param>
        /// <param name="customerScoreService"></param>
        public SortedCustomerScoreService(IScoreRankService scoreRankService, ICustomerScoreService customerScoreService)
        {
            _sortedScores = scoreRankService;
            _scores = customerScoreService;
        }


        /// <summary>
        /// 更新排序后的分数
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="score"></param>
        public void UpdateSortedScores(long customerId, double score)
        {
            if (_scores.TryGetValue(customerId, out var customerScore))
            {
                customerScore.Score = score;
                _sortedScores.AddOrUpdate(customerScore);
            }
        }

        /// <summary>
        /// 获取排名区间内的用户
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public virtual IEnumerable<CustomerScoreRankResponse> GetCustomersByRank(int start, int end)
        {
            var customerScores = _sortedScores.GetRange(start, end);
            if (IgnoreNegative)
            {
                customerScores = customerScores.Where(x => x.Score > 0);
            }
            var result = new List<CustomerScoreRankResponse>();
            int index = start;
            foreach (var item in customerScores)
            {
                result.Add(new CustomerScoreRankResponse
                {
                    CustomerId = item.CustomerId,
                    Score = item.Score,
                    Rank = index
                });
                index++;
            }
            return result;
        }
    }
}