using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using WhatsUp.Aggregator.Services;

namespace WhatsUp.Tests.UnitTests.Services.Utilities;

public class RouteKeyServiceTests
{
    private readonly RouteKeyService _service;

    public RouteKeyServiceTests()
    {
        var configurationMock = new Mock<IConfiguration>();
        _service = new RouteKeyService(configurationMock.Object);
    }

    [Fact]
    public void FindRouteByKey_ShouldReturnRoute_WhenKeyIsValid()
    {
        // Act
        var result = _service.FindRouteByKey("weather");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("weather");
    }

    [Fact]
    public void FindRouteByKey_ShouldReturnFail_WhenKeyIsInvalid()
    {
        // Act
        var result = _service.FindRouteByKey("invalid");

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("Route for key 'invalid' not found");
    }

    [Fact]
    public void FindKeyByRoute_ShouldReturnKey_WhenRouteIsValid()
    {
        // Act
        var result = _service.FindKeyByRoute("/weather");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("weather");
    }

    [Fact]
    public void FindKeyByRoute_ShouldReturnFail_WhenRouteIsInvalid()
    {
        // Act
        var result = _service.FindKeyByRoute("/invalid");

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("Key for route '/invalid' not found");
    }
}