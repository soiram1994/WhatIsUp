using WhatsUp.Aggregator.Helpers;

namespace WhatsUp.Tests.UnitTests.Functions;

public class UriHelperTests
{
    [Fact]
    public void AddKeyToUri_ShouldReturnFail_WhenKeyIsNull()
    {
        // Arrange
        var uri = new Uri("http://example.com");

        // Act
        var result = uri.AddKeyToUri(null, "value");

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Key is null or empty", result.Errors[0].Message);
    }

    [Fact]
    public void AddKeyToUri_ShouldReturnFail_WhenValueIsNull()
    {
        // Arrange
        var uri = new Uri("http://example.com");

        // Act
        var result = uri.AddKeyToUri("key", null);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Value is null or empty", result.Errors[0].Message);
    }

    [Fact]
    public void AddKeyToUri_ShouldReturnOk_WhenKeyAndValueAreValid()
    {
        // Arrange
        var uri = new Uri("http://example.com");

        // Act
        var result = uri.AddKeyToUri("key", "value");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("http://example.com/?key=value", result.Value.ToString());
    }
}