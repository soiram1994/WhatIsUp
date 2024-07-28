using FluentResults;
using WhatsUp.Aggregator.DTOs;
using WhatsUp.Aggregator.Models;

namespace WhatsUp.Aggregator.Services.Tracking;

public interface IResponseTimeTrackingService
{
    Task<Result> TrackResponseTimeAsync(string requestPath, long elapsedMilliseconds);
    Task<Result<IReadOnlyCollection<ResponseStatisticsDTO>>> GetResponseStatisticsAsync(string route);
}