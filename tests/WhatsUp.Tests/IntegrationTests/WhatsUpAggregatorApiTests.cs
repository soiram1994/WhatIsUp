using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using WhatsUp.Gateway.Models;

namespace WhatsUp.Tests.IntegrationTests;

public class WhatsUpAggregatorApiTests(WebApplicationFactory<Program> factory)
{
    [Fact(DisplayName = "Aggregator returns correct results")]
    public async Task AggregatorReturnsCorrectResults()
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/whatsup");

        // Act
        var response = await client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<WhatsUpDTO>>();
        result.Should().NotBeNull();
        result.Count().Should().BeGreaterThan(0);
    }
}