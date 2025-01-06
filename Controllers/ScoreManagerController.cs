using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Yzh.Bosai.Net.ScoreManager.Application.Service;

namespace Yzh.Bosai.Net.ScoreManager.Controllers
{
    [ApiController]
    [Route("api/scoremanager")]
    public class ScoreManagerController : ControllerBase
    {
        /// <summary>
        /// 积分管理核心服务
        /// </summary>
        private readonly IScoreManagerCoreService _scoreService;

        /// <summary>
        /// 排行榜服务
        /// </summary>
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
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("customer/{customerId}/score/{score}")]
        public async Task<IActionResult> UpdateScore([FromRoute] long customerId, [FromRoute, Range(-1000, 1000, ErrorMessage = "Score must be between -1000 and 1000.")] double score)
        {
            await _scoreService.UpdateScore(customerId, score);
            return Ok(_scoreService.GetCustomerById(customerId, null, null));
        }


        /// <summary>
        /// 获取排名区间内的用户
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
        /// 获取指定用户的积分信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        [HttpGet("leaderboard/{customerId}")]
        public IActionResult GetCustomerById([FromRoute] long customerId, [FromQuery] int high, [FromQuery] int low)
        {
            var customer = _sortedCustomerScoreService.GetCustomerRankWithNeighbors(customerId, high, low);
            return customer != null ? Ok(customer) : NotFound();
        }
    }
}
