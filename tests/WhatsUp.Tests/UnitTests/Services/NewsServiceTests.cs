using WhatsUp.Aggregator.Services;

namespace WhatsUp.Tests.UnitTests.Services;

public class NewsApiMiddlemanServiceTests
{
    private readonly NewsApiMiddlemanService _service = new();

    [Fact]
    public void AdjustRequest_ShouldReturnFail_WhenRequestUriIsNull()
    {
        // Arrange
        var request = new HttpRequestMessage();

        // Act
        var result = _service.AdjustRequest(request);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Request URI is null.", result.Errors[0].Message);
    }

    [Fact]
    public void AdjustRequest_ShouldReturnFail_WhenApiKeyIsMissing()
    {
        // Arrange
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("http://example.com")
        };

        // Act
        var result = _service.AdjustRequest(request);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("News API key is missing.", result.Errors[0].Message);
    }

    [Fact]
    public void AdjustRequest_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        Environment.SetEnvironmentVariable("NEWS_API_KEY", "test_key");
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("http://example.com")
        };

        // Act
        var result = _service.AdjustRequest(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("WhatsUp Gateway", request.Headers.UserAgent.ToString());
        Assert.Contains("apiKey=test_key", request.RequestUri.Query);
    }

    [Fact]
    public void HandleResponse_ShouldReturnFail_WhenResponseIsNotSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

        // Act
        var result = _service.HandleResponse(response);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("News API request failed.", result.Errors[0].Message);
    }

    [Fact]
    public void HandleResponse_ShouldReturnOk_WhenResponseIsSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

        // Act
        var result = _service.HandleResponse(response);

        // Assert
        Assert.True(result.IsSuccess);
    }
}