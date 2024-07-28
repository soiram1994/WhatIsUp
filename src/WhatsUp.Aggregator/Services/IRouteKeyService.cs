using FluentResults;

namespace WhatsUp.Aggregator.Services;

public interface IRouteKeyService
{
    Result<string> FindRouteByKey(string key);
    Result<string> FindKeyByRoute(string routeTemplate);
}