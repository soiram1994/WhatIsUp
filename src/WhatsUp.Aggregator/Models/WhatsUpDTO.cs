namespace WhatsUp.Aggregator.Models;

public class WhatsUpDTO
{
    public WeatherDTO Weather { get; set; }
    public CatFactDTO Fact { get; set; }
    public NewsDTO[] News { get; set; }
}