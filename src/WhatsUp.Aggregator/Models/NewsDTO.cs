using System.Text.Json.Serialization;

namespace WhatsUp.Aggregator.Models;

public class NewsDTO
{
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("url")] public string Url { get; set; }
}