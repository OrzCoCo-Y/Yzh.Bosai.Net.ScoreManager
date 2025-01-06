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

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scoreManager"></param>
        public CustomerScoreChangedHandler(ISortedCustomerScoreService scoreManager)
        {
            _scoreManager = scoreManager;
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

                // 调用 ScoreManagerCoreService 来更新排序集合
                _scoreManager.UpdateSortedScores(@event.CustomerId, @event.Score);

                // 这里可以添加其他逻辑，例如更新数据库、发送通知等。

                await Task.CompletedTask; // 如果没有异步操作，使用这个来满足接口要求
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update sorted scores: {ex.Message}");
                throw; // 或者根据需要选择不抛出异常
            }
        }
    }
}
