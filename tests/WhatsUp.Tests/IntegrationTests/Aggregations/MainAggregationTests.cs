using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using WhatsUp.Aggregator.DTOs;

namespace WhatsUp.Tests.IntegrationTests.Aggregations;

public class MainAggregationTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact(DisplayName =
        "Weather and news are received from the Weather and News APIs. Cat facts are received from the Cats API.")]
    public async Task GetWeatherAndNews_ReturnsWeatherAndNews()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/whatsup/London");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<WhatsUpDTO>();
        result.Should().NotBeNull();
        result.Weather.Should().NotBeNull();
        result.News.Should().NotBeNull();
        result.Fact.Should().NotBeNull();
    }
}