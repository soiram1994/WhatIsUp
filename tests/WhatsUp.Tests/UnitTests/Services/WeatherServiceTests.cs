using WhatsUp.Aggregator.Services;

namespace WhatsUp.Tests.UnitTests;

public class WeatherApiMiddlemanServiceTests
{
    private readonly WeatherApiMiddlemanService _service = new();

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
        Assert.Equal("Weather API key is missing.", result.Errors[0].Message);
    }

    [Fact]
    public void AdjustRequest_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        Environment.SetEnvironmentVariable("WEATHER_API_KEY", "test_key");
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("http://example.com")
        };

        // Act
        var result = _service.AdjustRequest(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Contains("appid=test_key", request.RequestUri.Query);
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
        Assert.Equal("Weather API request failed.", result.Errors[0].Message);
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