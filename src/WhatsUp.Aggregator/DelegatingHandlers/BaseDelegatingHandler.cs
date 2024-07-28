using System.Net;
using WhatsUp.Aggregator.Services.Middlemen;

namespace WhatsUp.Aggregator.DelegatingHandlers;

/// <summary>
///     Individual delegating handler that is responsible for adjusting the request message and handling the response
/// </summary>
public abstract class BaseDelegatingHandler<TMiddleman>(ILogger logger, TMiddleman middleman) : DelegatingHandler
    where TMiddleman : class, IMiddlemanService
{
    private const string SendingSuccessMessage = "Received successful response from {Uri}: {Message}";
    private const string SendingErrorMessage = "Received unsuccessful response from {Uri}: {Message}";
    private const string AdjustingRequestSuccessMessage = "Adjusted request message for {Uri}";
    private const string AdjustingRequestErrorMessage = "Failed to adjust request message for {Uri}";

    protected readonly ILogger Logger = logger;
    protected readonly TMiddleman Middleman = middleman;


    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Adjust the request message
        var adjustedRequestResult = Middleman.AdjustRequest(request);
        if (adjustedRequestResult.IsFailed)
        {
            var errors = string.Join(", ", adjustedRequestResult.Errors.Select(e => e.Message));
            Logger.LogError(SendingErrorMessage, request.RequestUri, errors);
            return CreateErrorResponse($"{AdjustingRequestErrorMessage}", request.RequestUri);
        }

        Logger.LogDebug(AdjustingRequestSuccessMessage, request.RequestUri);
        var adjustedRequest = adjustedRequestResult.Value;

        // Send the request
        Logger.LogDebug("Sending request to {Uri}", adjustedRequest.RequestUri);
        var response = await base.SendAsync(adjustedRequest, cancellationToken);
        var responseHandingResult = Middleman.HandleResponse(response);
        if (responseHandingResult.IsFailed)
        {
            var errors = string.Join(", ", responseHandingResult.Errors.Select(e => e.Message));
            Logger.LogWarning(SendingErrorMessage, adjustedRequest.RequestUri, errors);
            return CreateErrorResponse(responseHandingResult.Errors.First().Message, adjustedRequest.RequestUri);
        }

        Logger.LogDebug(SendingSuccessMessage, adjustedRequest.RequestUri, "");
        return responseHandingResult.Value;
    }

    protected virtual HttpResponseMessage CreateErrorResponse(string message, Uri uri)
    {
        Logger.LogWarning(SendingErrorMessage, uri, message);
        return new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent(message)
        };
    }
}