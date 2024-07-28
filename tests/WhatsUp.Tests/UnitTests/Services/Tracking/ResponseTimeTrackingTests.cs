using FluentAssertions;
using FluentResults;
using Moq;
using WhatsUp.Aggregator.Entities;
using WhatsUp.Aggregator.Services.Tracking;

namespace WhatsUp.Tests.UnitTests.Services.Tracking;

public class ResponseTimeTrackingServiceTests
{
    private readonly Mock<IResponseTrackingRepo> _repoMock;
    private readonly ResponseTimeTrackingService _service;

    public ResponseTimeTrackingServiceTests()
    {
        _repoMock = new Mock<IResponseTrackingRepo>();
        _service = new ResponseTimeTrackingService(_repoMock.Object);
    }

    [Fact]
    public async Task TrackResponseTimeAsync_ShouldReturnSuccess_WhenEntryIsAdded()
    {
        // Arrange
        var requestPath = "/test";
        var elapsedMilliseconds = 200;
        _repoMock.Setup(repo => repo.AddEntityWithRuleAsync(It.IsAny<ResponseTrackerEntry>()))
            .ReturnsAsync(Result.Ok());

        // Act
        var result = await _service.TrackResponseTimeAsync(requestPath, elapsedMilliseconds);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _repoMock.Verify(repo => repo.AddEntityWithRuleAsync(It.Is<ResponseTrackerEntry>(
                entry => entry.RequestPath == requestPath && entry.ElapsedMilliseconds == elapsedMilliseconds)),
            Times.Once);
    }

    [Fact]
    public async Task TrackResponseTimeAsync_ShouldReturnFail_WhenEntryIsNotAdded()
    {
        // Arrange
        var requestPath = "/test";
        var elapsedMilliseconds = 200;
        _repoMock.Setup(repo => repo.AddEntityWithRuleAsync(It.IsAny<ResponseTrackerEntry>()))
            .ReturnsAsync(Result.Fail("Error"));

        // Act
        var result = await _service.TrackResponseTimeAsync(requestPath, elapsedMilliseconds);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("Error");
    }

    [Fact]
    public async Task GetResponseStatisticsAsync_ShouldReturnStatistics_WhenEntriesExist()
    {
        // Arrange
        var route = "/test";
        var entries = new List<ResponseTrackerEntry>
        {
            new(route, 100),
            new(route, 200),
            new(route, 300)
        };
        _repoMock.Setup(repo => repo.GetEntriesForRouteAsync(route))
            .ReturnsAsync(Result.Ok((IReadOnlyCollection<ResponseTrackerEntry>)entries));

        // Act
        var result = await _service.GetResponseStatisticsAsync(route);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var statistics = result.Value;
        statistics.Should().HaveCount(1);
        var stat = statistics.First();
        stat.RequestPath.Should().Be(route);
        stat.AverageResponseTime.Should().Be(200);
        stat.MinResponseTime.Should().Be(100);
        stat.MaxResponseTime.Should().Be(300);
        stat.TotalRequests.Should().Be(3);
        stat.Rank.Should().Be("Medium");
    }

    [Fact]
    public async Task GetResponseStatisticsAsync_ShouldReturnFail_WhenNoEntriesExist()
    {
        // Arrange
        var route = "/test";
        _repoMock.Setup(repo => repo.GetEntriesForRouteAsync(route))
            .ReturnsAsync(Result.Fail<IReadOnlyCollection<ResponseTrackerEntry>>("Error"));

        // Act
        var result = await _service.GetResponseStatisticsAsync(route);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("Error");
    }
}