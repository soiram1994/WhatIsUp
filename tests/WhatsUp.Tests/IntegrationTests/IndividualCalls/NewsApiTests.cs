using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using WhatsUp.Aggregator;

namespace WhatsUp.Tests.IntegrationTests.IndividualCalls;

public class NewsApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact(DisplayName = "News are received from the News API.")]
    public async Task GetNewsAsync()
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/news/London");

        // Act
        var response = await client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        result.Should().NotBeNull();
    }
}