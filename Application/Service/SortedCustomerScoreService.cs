using Yzh.Bosai.Net.ScoreManager.Application.Dto;
using Yzh.Bosai.Net.ScoreManager.Domain.Service;

namespace Yzh.Bosai.Net.ScoreManager.Application.Service
{
    /// <summary>
    /// 排序后的用户积分服务
    /// </summary>
    public class SortedCustomerScoreService : ISortedCustomerScoreService
    {
        /// <summary>
        /// 排名服务
        /// </summary>
        private readonly IScoreRankService _sortedScores;

        /// <summary>
        /// 客户积分服务
        /// </summary>
        private readonly ICustomerScoreService _scores;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<SortedCustomerScoreService> _logger;

        /// <summary>
        /// 是否忽略负分
        /// </summary>
        public virtual bool IgnoreNegative => true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scoreRankService">排名服务</param>
        /// <param name="customerScoreService">客户积分服务</param>
        /// <param name="logger">日志</param>
        public SortedCustomerScoreService(IScoreRankService scoreRankService, 
                                          ICustomerScoreService customerScoreService,
                                          ILogger<SortedCustomerScoreService> logger)
        {
            _sortedScores = scoreRankService;
            _scores = customerScoreService;
            _logger = logger;
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
            if (customerScores.Any())
            {
                if (IgnoreNegative)
                {
                    customerScores = customerScores.Where(x => x.Score > 0);
                }
                var result = new List<CustomerScoreRankResponse>();

                int index = start > 0 ? start : 1;
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

            return null;
        }

        /// <summary>
        /// 获取指定用户的上下邻居
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        public List<CustomerScoreRankResponse> GetCustomerRankWithNeighbors(long customerId, int highNeighbors = 0, int lowNeighbors = 0)
        {
            // 假设我们有一个方法可以得到用户的排名
            var rankResult = _sortedScores.GetRankAByCustomerId(customerId);

            if (rankResult.rank <= 0)
            {
                _logger.LogError($"======  GetCustomerRankWithNeighbors Error ! {rankResult.message} ========");
                return null; // 或者抛出异常，取决于业务逻辑
            }

            // 邻居
            var result = GetCustomersByRank(rankResult.rank - highNeighbors, rankResult.rank + lowNeighbors).ToList();

            return result;
        }
    }
}