using WhatsUp.Aggregator.Services.Middlemen;

namespace WhatsUp.Aggregator.DelegatingHandlers;

public class NewsApiDelegatingHandler(
    ILogger<NewsApiDelegatingHandler> logger,
    NewsApiMiddlemanService newsApiMiddlemanService)
    : BaseDelegatingHandler<NewsApiMiddlemanService>(logger, newsApiMiddlemanService)
{
}