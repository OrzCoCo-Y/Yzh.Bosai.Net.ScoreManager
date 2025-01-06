using Microsoft.AspNetCore.Mvc;
using Yzh.Bosai.Net.ScoreManager.Application.Service;
using Yzh.Bosai.Net.ScoreManager.Infrastructure.Services;

namespace Yzh.Bosai.Net.ScoreManager.Controllers
{
    [ApiController]
    [Route("api/scoremanager")]
    public class ScoreManagerController : ControllerBase
    {
        private readonly IScoreManagerCoreService _scoreService;

        private readonly ISortedCustomerScoreService _sortedCustomerScoreService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scoreService"></param>
        /// <param name="sortedCustomerScoreService"></param>
        public ScoreManagerController(IScoreManagerCoreService scoreService, ISortedCustomerScoreService sortedCustomerScoreService)
        {
            _scoreService = scoreService;
            _sortedCustomerScoreService = sortedCustomerScoreService;
        }

        /// <summary>
        /// 更新用户积分 ，无对于用户的积分记录时会创建新的积分记录
        /// update the score of a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        [HttpPost("customer/{customerId}/score/{score}")]
        public async Task<IActionResult> UpdateScore([FromRoute] long customerId, [FromRoute] double score = 0)
        {
            await _scoreService.UpdateScore(customerId, score);
            return Ok(_scoreService.GetCustomerById(customerId, null, null));
        }

        /// <summary>
        /// 获取排名区间内的用户
        /// get customers in a rank range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet("leaderboard")]
        public IActionResult GetCustomersByRank([FromQuery] int start, [FromQuery] int end)
        {
            var customers = _sortedCustomerScoreService.GetCustomersByRank(start, end);
            return Ok(customers);
        }

        /// <summary>
        /// 获取指定用户的排名
        /// get the rank of a specific customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        [HttpGet("leaderboard/{customerId}")]
        public IActionResult GetCustomerById([FromRoute] long customerId, [FromQuery] int? high, [FromQuery] int? low)
        {
            var customer = _scoreService.GetCustomerById(customerId, high, low);
            return customer != null ? (IActionResult)Ok(customer) : NotFound();
        }
    }
}
