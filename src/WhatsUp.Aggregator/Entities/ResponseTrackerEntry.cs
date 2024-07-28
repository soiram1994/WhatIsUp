using System.ComponentModel.DataAnnotations;

namespace WhatsUp.Aggregator.Entities;

public class ResponseTrackerEntry(string requestPath, long elapsedMilliseconds)
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public string RequestPath { get; set; } = requestPath;
    public long ElapsedMilliseconds { get; set; } = elapsedMilliseconds;
}