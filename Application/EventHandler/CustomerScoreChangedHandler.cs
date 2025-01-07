using Microsoft.AspNetCore.SignalR;
using Yzh.Bosai.Net.ScoreManager.Api.Application.SignalR;
using Yzh.Bosai.Net.ScoreManager.Application.Dto;
using Yzh.Bosai.Net.ScoreManager.Application.Event;
using Yzh.Bosai.Net.ScoreManager.Application.Service;

namespace Yzh.Bosai.Net.ScoreManager.Application.EventHandler
{
    /// <summary>
    /// 处理用户积分变更事件的接口
    /// </summary>
    public class CustomerScoreChangedHandler : IEventHandler<CustomerScoreChangedDto>
    {
        private readonly ISortedCustomerScoreService _scoreManager;
        private readonly IHubContext<ScoreRankHub> _hubContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scoreManager"></param>
        public CustomerScoreChangedHandler(ISortedCustomerScoreService scoreManager, IHubContext<ScoreRankHub> hubContext)
        {
            _scoreManager = scoreManager;
            _hubContext = hubContext;
        }

        /// <summary>
        /// 处理用户积分变更事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task HandleAsync(CustomerScoreChangedDto @event)
        {
            try
            {
                Console.WriteLine($"Handling event: Customer ID: {@event.CustomerId}, current Score: {@event.Score}");

                // 更新排序集合
                _scoreManager.UpdateSortedScores(@event.CustomerId, @event.Score);

                // 这里可以添加其他逻辑，例如发送通知
                // 通过SignlaR发送通知
                // 获取最新的排名信息
                var rankInfo = _scoreManager.GetCustomersByRank(0, -1);

                if (rankInfo != null)
                {
                    // 通过 SignalR 发送通知给所有客户端
                    await _hubContext.Clients.All.SendAsync("ReceiveRankUpdate", rankInfo);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update sorted scores: {ex.Message}");
                throw;
            }
        }
    }
}
