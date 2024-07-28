using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using WhatsUp.Aggregator.Aggregations;
using WhatsUp.Aggregator.DelegatingHandlers;
using WhatsUp.Aggregator.Services;

namespace WhatsUp.Aggregator;

public static class ProgramExtensions
{
    public static IServiceCollection ConfigureOcelot(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOcelot(configuration)
            .AddCacheManager(x => { x.WithDictionaryHandle(); })
            .AddSingletonDefinedAggregator<MainAggregator>()
            .AddDelegatingHandler<WeatherApiDelegatingHandler>()
            .AddDelegatingHandler<NewsApiDelegatingHandler>();
        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<WeatherApiMiddlemanService>();
        services.AddSingleton<NewsApiMiddlemanService>();
        return services;
    }
}