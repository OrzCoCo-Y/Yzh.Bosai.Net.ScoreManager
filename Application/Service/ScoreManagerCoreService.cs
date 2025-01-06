using Yzh.Bosai.Net.ScoreManager.Application.Dto;
using Yzh.Bosai.Net.ScoreManager.Application.Event;
using Yzh.Bosai.Net.ScoreManager.Domain.Service;
using Yzh.Bosai.Net.ScoreManager.Shared;

namespace Yzh.Bosai.Net.ScoreManager.Application.Service
{
    /// <summary>
    /// 积分管理核心服务
    /// </summary>
    public class ScoreManagerCoreService : IScoreManagerCoreService
    {
        /// <summary>
        /// 积分服务
        /// </summary>
        private readonly ICustomerScoreService _scores;

        /// <summary>
        /// 事件总线
        /// </summary>
        private readonly IEventBus _eventBus;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="customerScores"></param>
        public ScoreManagerCoreService(IEventBus eventBus, ICustomerScoreService customerScores)
        {
            _eventBus = eventBus;
            _scores = customerScores;
        }

        /// <summary>
        /// 获取用户积分
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="highNeighbors"></param>
        /// <param name="lowNeighbors"></param>
        /// <returns></returns>
        public CustomerScore GetCustomerById(long customerId, int? highNeighbors, int? lowNeighbors)
        {
            if (_scores.TryGetValue(customerId, out var customer))
            {
                return customer;
            }

            return null;
        }

        /// <summary>
        /// 更新用户积分
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="scoreChange"></param>
        /// <returns></returns>
        public async Task UpdateScore(long customerId, double scoreChange)
        {
            if (_scores.TryUpdate(customerId, scoreChange, out var customerScore))
            {
                await _eventBus.PublishAsync(new CustomerScoreChangedDto(customerId, customerScore.Score));
            }
            else
            {
                var newScore = new CustomerScore { CustomerId = customerId, Score = scoreChange };
                if (_scores.TryAdd(customerId, newScore))
                {
                    await _eventBus.PublishAsync(new CustomerScoreChangedDto(customerId, newScore.Score));
                }
            }
        }
    }
}
