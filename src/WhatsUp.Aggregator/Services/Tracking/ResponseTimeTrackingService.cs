using FluentResults;
using WhatsUp.Aggregator.DTOs;
using WhatsUp.Aggregator.Entities;

namespace WhatsUp.Aggregator.Services.Tracking;

public class ResponseTimeTrackingService(IResponseTrackingRepo repo) : IResponseTimeTrackingService
{
    public async Task<Result> TrackResponseTimeAsync(string requestPath, long elapsedMilliseconds)
    {
        var newEntry = new ResponseTrackerEntry(requestPath, elapsedMilliseconds);

        var result = await repo.AddEntityWithRuleAsync(newEntry);
        return result;
    }

    public async Task<Result<IReadOnlyCollection<ResponseTrackerDTO>>> GetResponseStatisticsAsync(string route)
    {
        var entries = await repo.GetEntriesForRouteAsync(route);
        if (entries.IsFailed) return entries.ToResult<IReadOnlyCollection<ResponseTrackerDTO>>();

        var statistics = entries.Value
            .GroupBy(x => x.RequestPath)
            .Select(x =>
            {
                var average = x.Average(e => e.ElapsedMilliseconds);
                var min = x.Min(e => e.ElapsedMilliseconds);
                var max = x.Max(e => e.ElapsedMilliseconds);
                var count = x.Count();
                var rank = average switch
                {
                    < 100 => "Fast",
                    < 500 => "Medium",
                    _ => "Slow"
                };
                return new ResponseTrackerDTO
                {
                    RequestPath = x.Key,
                    AverageResponseTime = average,
                    MinResponseTime = min,
                    MaxResponseTime = max,
                    TotalRequests = count,
                    Rank = rank
                };
            })
            .ToList();

        return Result.Ok<IReadOnlyCollection<ResponseTrackerDTO>>(statistics);
    }
}