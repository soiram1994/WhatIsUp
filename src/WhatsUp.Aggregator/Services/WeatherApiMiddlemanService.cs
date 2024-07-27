using System.Web;
using FluentResults;
using WhatsUp.Gateway.Helpers;

namespace WhatsUp.Gateway.Services;

public class WeatherApiMiddlemanService : IMiddlemanService
{
    public Result<HttpRequestMessage> AdjustRequest(HttpRequestMessage request)
    {
        if (request.RequestUri == null)
        {
            return Result.Fail("Request URI is null.");
        }

        // Get the API key from the environment variables.
        var key = Environment.GetEnvironmentVariable("WEATHER_API_KEY");
        if (string.IsNullOrEmpty(key))
        {
            return Result.Fail("Weather API key is missing.");
        }

        // Add the API key to the query string.
        var uriResult = request.RequestUri.AddKeyToUri("appid", key);

        // Check if the URI is valid.
        return uriResult.IsFailed
            ? Result.Fail(uriResult.Errors)
            : Result.Ok(request);
    }

    public Result<HttpResponseMessage> HandleResponse(HttpResponseMessage response)
    {
        // Check if the response is successful.
        return !response.IsSuccessStatusCode
            ? Result.Fail("Weather API request failed.")
            : Result.Ok(response);
    }
}