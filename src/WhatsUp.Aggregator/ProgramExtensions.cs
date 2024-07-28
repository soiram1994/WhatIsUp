using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Polly;
using WhatsUp.Aggregator.Aggregations;
using WhatsUp.Aggregator.DelegatingHandlers;
using WhatsUp.Aggregator.Entities;
using WhatsUp.Aggregator.Repos;
using WhatsUp.Aggregator.Services;
using WhatsUp.Aggregator.Services.Middlemen;
using WhatsUp.Aggregator.Services.Tracking;
using WhatsUp.Aggregator.Services.Utilities;

namespace WhatsUp.Aggregator;

public static class ProgramExtensions
{
    public static IServiceCollection RegisterOcelot(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOcelot(configuration)
            .AddCacheManager(x => { x.WithDictionaryHandle(); })
            .AddPolly()
            .AddSingletonDefinedAggregator<MainAggregator>()
            .AddDelegatingHandler<WeatherApiDelegatingHandler>()
            .AddDelegatingHandler<NewsApiDelegatingHandler>();
        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddSingleton<WeatherApiMiddlemanService>()
            .AddSingleton<NewsApiMiddlemanService>()
            .AddSingleton<IResponseTimeTrackingService, ResponseTimeTrackingService>()
            .AddSingleton<IRouteKeyService, RouteKeyService>();

        return services;
    }

    public static IServiceCollection RegisterRepos(this IServiceCollection services)
    {
        services
            .AddEntityFrameworkInMemoryDatabase()
            .AddDbContext<WhatsUpDbContext>(ServiceLifetime.Singleton)
            .AddSingleton<IResponseTrackingRepo, ResponseTrackingRepo>();
        return services;
    }
}