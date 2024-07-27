using System.Web;
using FluentResults;

namespace WhatsUp.Aggregator.Helpers;

public static class UriHelper
{
    public static Result<Uri> AddKeyToUri(this Uri uri, string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return Result.Fail("Key is null or empty");
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail("Value is null or empty");
        }

        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query[key] = value;
        uriBuilder.Query = query.ToString();

        return Result.Ok(uriBuilder.Uri);
    }
}