using Yzh.Bosai.Net.ScoreManager.Application.Dto;
using Yzh.Bosai.Net.ScoreManager.Application.Event;
using Yzh.Bosai.Net.ScoreManager.Application.EventHandler;
using Yzh.Bosai.Net.ScoreManager.Application.Service;
using Yzh.Bosai.Net.ScoreManager.Domain.Service;
using Yzh.Bosai.Net.ScoreManager.Infrastructure.Services;

namespace Yzh.Bosai.Net.ScoreManager
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public static class ServiceRegistrations
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(IServiceCollection services)
        {
            // 事件总线
            services.AddSingleton<IEventBus, InMemoryEventBus>();

            // 数据存储-模拟实现-单例模式 -- 仅用于演示，实际应用中应该使用数据库存储
            services.AddSingleton<ICustomerScoreService, CustomerScoresService>();
            services.AddSingleton<IScoreRankService, ScoreRankService>();

            // 服务
            services.AddSingleton<ISortedCustomerScoreService, SortedCustomerScoreService>();
            services.AddSingleton<IScoreManagerCoreService, ScoreManagerCoreService>();
            services.AddSingleton<IEventHandler<CustomerScoreChangedDto>, CustomerScoreChangedHandler>();
        }
    }
}
