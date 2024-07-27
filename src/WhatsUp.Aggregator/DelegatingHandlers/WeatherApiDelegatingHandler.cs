using WhatsUp.Aggregator.Services;

namespace WhatsUp.Aggregator.DelegatingHandlers;

public class WeatherApiDelegatingHandler(
    ILogger<WeatherApiDelegatingHandler> logger,
    WeatherApiMiddlemanService weatherApiMiddlemanService)
    : BaseDelegatingHandler<WeatherApiMiddlemanService>(logger, weatherApiMiddlemanService)
{
}