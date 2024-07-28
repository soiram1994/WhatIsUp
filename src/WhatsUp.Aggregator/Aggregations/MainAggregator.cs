using System.Net;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using WhatsUp.Aggregator.DTOs;
using WhatsUp.Aggregator.Models;

namespace WhatsUp.Aggregator.Aggregations;

public class MainAggregator : IDefinedAggregator
{
    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responseHttpContexts)
    {
        var responses = responseHttpContexts.Select(x => x.Items.DownstreamResponse()).ToArray();
        // In case of error send in the string content the error messages
        if (responses.Any(x => x.StatusCode != HttpStatusCode.OK))
        {
            var errorTasks = responses.Where(x => x.StatusCode != HttpStatusCode.OK)
                .Select(x => x.Content.ReadAsStringAsync())
                .ToList();
            var errors = await Task.WhenAll(errorTasks);
            return new DownstreamResponse(
                new StringContent(JsonConvert.SerializeObject(errors)),
                HttpStatusCode.BadRequest,
                responses.SelectMany(x => x.Headers).ToList(),
                "application/json");
        }

        var result = new WhatsUpDTO();

        try
        {
            foreach (var responseHttpContext in responseHttpContexts)
            {
                var response = responseHttpContext.Items.DownstreamResponse();
                var content = await response.Content.ReadAsStringAsync();

                var downstreamPath = responseHttpContext.Items.DownstreamRequest().Request.RequestUri.AbsoluteUri
                    .ToLower();
                switch (downstreamPath)
                {
                    case var _ when downstreamPath.Contains("api.openweathermap.org"):
                        result.Weather = JsonConvert.DeserializeObject<WeatherDTO>(content) ?? null;
                        break;
                    case var _ when downstreamPath.Contains("catfact.ninja"):
                        result.Fact = JsonConvert.DeserializeObject<CatFactDTO>(content) ?? null;
                        break;
                    case var _ when downstreamPath.Contains("newsapi.org"):
                        result.News = JsonConvert.DeserializeObject<NewsDTO>(content);
                        break;
                    default:
                        return new DownstreamResponse(new StringContent("Invalid path"), HttpStatusCode.BadRequest,
                            responses.SelectMany(x => x.Headers).ToList(), "application/json");
                }
            }
        }
        catch (Exception e)
        {
            return new DownstreamResponse(
                new StringContent(JsonConvert.SerializeObject(e.Message)),
                HttpStatusCode.InternalServerError,
                responses.SelectMany(x => x.Headers).ToList(),
                "application/json");
        }


        return new DownstreamResponse(
            new StringContent(JsonConvert.SerializeObject(result)),
            HttpStatusCode.OK,
            responses.SelectMany(x => x.Headers).ToList(),
            "application/json");
    }
}