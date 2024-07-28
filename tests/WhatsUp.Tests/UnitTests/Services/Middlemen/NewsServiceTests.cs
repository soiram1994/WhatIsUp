using System.Net;
using FluentAssertions;
using WhatsUp.Aggregator.Services.Middlemen;

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
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("Request URI is null.");
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
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("News API key is missing.");
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
        result.IsSuccess.Should().BeTrue();
        request.Headers.UserAgent.ToString().Should().Be("WhatsUp Gateway");
        request.RequestUri.Query.Should().Contain("apiKey=test_key");
    }

    [Fact]
    public void HandleResponse_ShouldReturnFail_WhenResponseIsNotSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

        // Act
        var result = _service.HandleResponse(response);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("News API request failed.");
    }

    [Fact]
    public void HandleResponse_ShouldReturnOk_WhenResponseIsSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage(HttpStatusCode.OK);

        // Act
        var result = _service.HandleResponse(response);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}