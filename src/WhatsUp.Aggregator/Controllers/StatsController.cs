using Microsoft.AspNetCore.Mvc;
using WhatsUp.Aggregator.Services;
using WhatsUp.Aggregator.Services.Tracking;

namespace WhatsUp.Aggregator.Controllers;

[Route("api/[controller]")]
public class StatsController(IResponseTimeTrackingService tracker, IRouteKeyService routeKeyService) : ControllerBase
{
    [HttpGet("{key}")]
    public async Task<IActionResult> Get(string key)
    {
        var routeResult = routeKeyService.FindRouteByKey(key);
        if (routeResult.IsFailed)
        {
            return BadRequest(routeResult.Errors);
        }

        var result = await tracker.GetResponseStatisticsAsync(routeResult.Value);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
}