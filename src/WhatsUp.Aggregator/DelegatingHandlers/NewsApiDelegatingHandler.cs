using System.Web;

namespace WhatsUp.Gateway.DelegatingHandlers;

public class NewsApiDelegatingHandler(ILogger<NewsApiDelegatingHandler> logger, IConfiguration configuration)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.RequestUri =
            AdjustUriWithApiKey(request.RequestUri ?? throw new InvalidOperationException("Request URI is null"));
        logger.LogInformation("Sending request to {Uri}", request.RequestUri);
        var response = await base.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Received unsuccessful response from {Uri}", request.RequestUri);
            return response;
        }

        logger.LogInformation("Received successful response from {Uri}", request.RequestUri);
        return response;
    }

    private Uri AdjustUriWithApiKey(Uri uri)
    {
        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["apiKey"] = configuration["NewsApiKey"];
        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }
}