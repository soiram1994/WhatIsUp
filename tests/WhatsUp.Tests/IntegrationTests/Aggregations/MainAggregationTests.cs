using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using WhatsUp.Aggregator.Models;

namespace WhatsUp.Tests.IntegrationTests.Aggregations;

public class MainAggregationTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetWeatherAndNews_ReturnsWeatherAndNews()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/whatsup/Athens");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<WhatsUpDTO>();
        result.Should().NotBeNull();
        result.Weather.Should().NotBeNull();
        result.News.Should().NotBeNull();
        result.Fact.Should().NotBeNull();
    }
}