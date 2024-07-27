using System.Text.Json.Serialization;

namespace WhatsUp.Gateway.Models;

public class NewsDTO : IAggregation
{
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("url")] public string Url { get; set; }
}

public interface IAggregation
{
}