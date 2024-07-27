using System.Web;
using WhatsUp.Gateway.Services;

namespace WhatsUp.Gateway.DelegatingHandlers;

public class WeatherApiDelegatingHandler(
    ILogger<WeatherApiDelegatingHandler> logger,
    WeatherApiMiddlemanService weatherApiMiddlemanService)
    : BaseDelegatingHandler<WeatherApiMiddlemanService>(logger, weatherApiMiddlemanService)
{
}