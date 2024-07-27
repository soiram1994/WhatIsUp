using WhatsUp.Aggregator.Services;

namespace WhatsUp.Aggregator.DelegatingHandlers;

public class NewsApiDelegatingHandler(
    ILogger<NewsApiDelegatingHandler> logger,
    NewsApiMiddlemanService newsApiMiddlemanService)
    : BaseDelegatingHandler<NewsApiMiddlemanService>(logger, newsApiMiddlemanService)
{
}