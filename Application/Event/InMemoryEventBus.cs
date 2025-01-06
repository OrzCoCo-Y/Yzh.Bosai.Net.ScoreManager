using Yzh.Bosai.Net.ScoreManager.Application.Dto;

namespace Yzh.Bosai.Net.ScoreManager.Application.Event
{
    /// <summary>
    /// 事件总线-内存实现
    /// </summary>
    public class InMemoryEventBus : IEventBus
    {
        /// <summary>
        /// 事件处理器
        /// </summary>
        private readonly IEnumerable<IEventHandler<CustomerScoreChangedDto>> _handlers;

        /// <summary>
        /// 构造函数 注入事件处理器
        /// </summary>
        /// <param name="handlers"></param>
        public InMemoryEventBus(IEnumerable<IEventHandler<CustomerScoreChangedDto>> handlers)
        {
            // 将 IEnumerable 转换为不可变列表，确保线程安全
            _handlers = handlers.ToList().AsReadOnly();
        }

        public async Task PublishAsync<T>(T @event) where T : class
        {
            var eventType = typeof(T).Name;

            // 只针对 CustomerScoreChangedDto 进行了硬编码  ， 正常情况下应该使用反射来处理
            if (eventType == nameof(CustomerScoreChangedDto))
            {
                var customerScoreChangedEvent = @event as CustomerScoreChangedDto;
                if (customerScoreChangedEvent != null)
                {
                    foreach (var handler in _handlers)
                    {
                        await handler.HandleAsync(customerScoreChangedEvent);
                    }
                }
            }
        }
    }
}
