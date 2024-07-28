using FluentResults;
using WhatsUp.Aggregator.Helpers;
using WhatsUp.Aggregator.Services.Middlemen;

namespace WhatsUp.Aggregator.Services;

public class WeatherApiMiddlemanService : IMiddlemanService
{
    public Result<HttpRequestMessage> AdjustRequest(HttpRequestMessage request)
    {
        if (request.RequestUri == null) return Result.Fail("Request URI is null.");

        var key = Environment.GetEnvironmentVariable("WEATHER_API_KEY");
        if (string.IsNullOrEmpty(key)) return Result.Fail("Weather API key is missing.");

        var uriResult = request.RequestUri.AddKeyToUri("appid", key);

        if (uriResult.IsFailed) return Result.Fail(uriResult.Errors);

        request.RequestUri = uriResult.Value;
        return Result.Ok(request);
    }

    public Result<HttpResponseMessage> HandleResponse(HttpResponseMessage response)
    {
        // Check if the response is successful.
        return !response.IsSuccessStatusCode
            ? Result.Fail("Weather API request failed.")
            : Result.Ok(response);
    }
}