namespace WhatsUp.Aggregator.DTOs;

public class ResponseTrackerDTO
{
    public string Key { get; set; }
    public string RequestPath { get; set; }
    public double AverageResponseTime { get; set; }
    public string Rank { get; set; }
    public long MaxResponseTime { get; set; }
    public long MinResponseTime { get; set; }
    public int TotalRequests { get; set; }
}