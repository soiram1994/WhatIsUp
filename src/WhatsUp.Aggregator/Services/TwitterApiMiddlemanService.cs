using System.Net.Http.Headers;
using FluentResults;

namespace WhatsUp.Gateway.Services;

public class TwitterApiMiddlemanService : IMiddlemanService
{
    public Result<HttpRequestMessage> AdjustRequest(HttpRequestMessage request)
    {
        var bearerToken = Environment.GetEnvironmentVariable("TWITTER_BEARER_TOKEN");
        if (string.IsNullOrEmpty(bearerToken))
        {
            return Result.Fail("Twitter bearer token is missing.");
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        return Result.Ok(request);
    }

    public Result<HttpResponseMessage> HandleResponse(HttpResponseMessage response)
    {
        return !response.IsSuccessStatusCode
            ? Result.Fail("Twitter API request failed.")
            : Result.Ok(response);
    }
}