using FluentResults;
using WhatsUp.Aggregator.DTOs;

namespace WhatsUp.Aggregator.Services.Tracking;

public interface IResponseTimeTrackingService
{
    Task<Result> TrackResponseTimeAsync(string requestPath, long elapsedMilliseconds);
    Task<Result<IReadOnlyCollection<ResponseTrackerDTO>>> GetResponseStatisticsAsync(string route);
}