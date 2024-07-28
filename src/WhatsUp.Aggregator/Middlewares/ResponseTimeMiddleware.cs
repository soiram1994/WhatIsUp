using System.Diagnostics;
using WhatsUp.Aggregator.Services.Tracking;

namespace WhatsUp.Aggregator.Middlewares;

public class ResponseTimeMiddleware(
    RequestDelegate next,
    IResponseTimeTrackingService responseTimeTrackingService,
    ILogger<ResponseTimeMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Value!.Contains("stats"))
        {
            return;
        }

        var stopwatch = Stopwatch.StartNew();
        await next(context);
        stopwatch.Stop();

        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

        var requestPath = context.Request.Path.Value.Split("/", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        var result = await responseTimeTrackingService.TrackResponseTimeAsync(requestPath, elapsedMilliseconds);
        if (result.IsFailed)
        {
            logger.LogError("Error while tracking response time. With messages: {Message}",
                string.Join(",", result.Errors));
        }
    }
}