using Ocelot.DependencyInjection;
using WhatsUp.Gateway.Aggregations;
using WhatsUp.Gateway.DelegatingHandlers;
using WhatsUp.Gateway.Services;

namespace WhatsUp.Gateway;

public static class ProgramExtensions
{
    public static IServiceCollection ConfigureOcelot(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOcelot(configuration)
            .AddSingletonDefinedAggregator<MainAggregator>()
            .AddDelegatingHandler<WeatherApiDelegatingHandler>()
            .AddDelegatingHandler<NewsApiDelegatingHandler>()
            .AddDelegatingHandler<TwitterApiDelegatingHandler>();
        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IMiddlemanService, WeatherApiMiddlemanService>();
        services.AddSingleton<IMiddlemanService, NewsApiMiddlemanService>();
        services.AddSingleton<IMiddlemanService, TwitterApiMiddlemanService>();
        return services;
    }
}