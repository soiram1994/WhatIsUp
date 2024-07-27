using System.Net.Http.Headers;
using System.Web;
using WhatsUp.Gateway.Services;

namespace WhatsUp.Gateway.DelegatingHandlers;

public class TwitterApiDelegatingHandler(
    ILogger<TwitterApiDelegatingHandler> logger,
    TwitterApiMiddlemanService twitterApiMiddlemanService)
    : BaseDelegatingHandler<TwitterApiMiddlemanService>(logger, twitterApiMiddlemanService)
{
}