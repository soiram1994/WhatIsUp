using FluentResults;

namespace WhatsUp.Aggregator.Services.Utilities;

public interface IRouteKeyService
{
    Result<string> FindRouteByKey(string key);
    Result<string> FindKeyByRoute(string routeTemplate);
}