using Microsoft.AspNetCore.SignalR;
using Yzh.Bosai.Net.ScoreManager.Application.Dto;
using Yzh.Bosai.Net.ScoreManager.Application.Service;

namespace Yzh.Bosai.Net.ScoreManager.Api.Application.SignalR
{
    public class ScoreRankHub : Hub
    {
        private readonly ISortedCustomerScoreService _scoreManager;

        public ScoreRankHub(ISortedCustomerScoreService scoreManager)
        {
            _scoreManager = scoreManager;
        }
        /// <summary>
        /// 加入连接的事件
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public IEnumerable<CustomerScoreRankResponse> GetRankList()
        {
            // 获取排名信息
            var rankInfo = _scoreManager.GetCustomersByRank(0, -1);

            return rankInfo;
        }
    }
}
