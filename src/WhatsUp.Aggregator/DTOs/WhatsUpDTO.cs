using WhatsUp.Aggregator.Models;

namespace WhatsUp.Aggregator.DTOs;

public class WhatsUpDTO
{
    public WeatherDTO Weather { get; set; }
    public CatFactDTO Fact { get; set; }
    public NewsDTO News { get; set; }
}