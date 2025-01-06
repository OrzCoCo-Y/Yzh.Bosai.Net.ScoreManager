using Yzh.Bosai.Net.ScoreManager.Domain.Service;

namespace Yzh.Bosai.Net.ScoreManager.Application.Service
{
    /// <summary>
    /// 排序后的用户积分服务 - 不忽略负分
    /// </summary>
    public class AllSortedCustomerScoreService : SortedCustomerScoreService
    {
        public AllSortedCustomerScoreService(IScoreRankService scoreRankService, ICustomerScoreService customerScoreService, ILogger<SortedCustomerScoreService> logger) : base(scoreRankService, customerScoreService, logger)
        {
        }

        /// <summary>
        /// 是否忽略负分
        /// </summary>
        public override bool IgnoreNegative => false;
    }
}
