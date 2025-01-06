using Yzh.Bosai.Net.ScoreManager.Domain.Service;

namespace Yzh.Bosai.Net.ScoreManager.Application.Service
{
    /// <summary>
    /// 排序后的用户积分服务 - 不忽略负分
    /// </summary>
    public class AllSortedCustomerScoreService : SortedCustomerScoreService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scoreRankService"></param>
        /// <param name="customerScoreService"></param>
        public AllSortedCustomerScoreService(IScoreRankService scoreRankService, ICustomerScoreService customerScoreService) : base(scoreRankService, customerScoreService)
        {
        }

        /// <summary>
        /// 是否忽略负分
        /// </summary>
        public override bool IgnoreNegative => false;
    }
}
