namespace Yzh.Bosai.Net.ScoreManager.Application.Event
{
    /// <summary>
    /// 事件处理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventHandler<T>
       where T : class
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task HandleAsync(T @event);
    }
}
