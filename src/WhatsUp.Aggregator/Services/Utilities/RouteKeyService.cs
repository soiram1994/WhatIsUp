using FluentResults;
using WhatsUp.Aggregator.Services.Utilities;

namespace WhatsUp.Aggregator.Services;

public class RouteKeyService : IRouteKeyService
{
    private readonly IConfiguration _configuration;

    public RouteKeyService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Result<string> FindRouteByKey(string key)
    {
        return key switch
        {
            "weather" => Result.Ok("weather"),
            "news" => Result.Ok("news"),
            "whatsup" => Result.Ok("whatsup"),
            "catfact" => Result.Ok("catfact"),
            _ => Result.Fail<string>($"Route for key '{key}' not found")
        };
    }

    public Result<string> FindKeyByRoute(string routeTemplate)
    {
        return routeTemplate switch
        {
            "/weather" => Result.Ok("weather"),
            "/news" => Result.Ok("news"),
            "/whatsup" => Result.Ok("whatsup"),
            "/catfact" => Result.Ok("catfact"),
            _ => Result.Fail<string>($"Key for route '{routeTemplate}' not found")
        };
    }
}