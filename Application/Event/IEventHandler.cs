namespace Yzh.Bosai.Net.ScoreManager.Application.Event
{
    public interface IEventHandler<T>
       where T : class
    {
        Task HandleAsync(T @event);
    }
}
