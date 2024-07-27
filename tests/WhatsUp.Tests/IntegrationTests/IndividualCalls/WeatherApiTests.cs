using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WhatsUp.Tests.IntegrationTests.IndividualCalls;

public class WeatherApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact(DisplayName = "Weather is received from the Weather API.")]
    public async Task GetWeatherAsync()
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/weather/London");

        // Act
        var response = await client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        result.Should().NotBeNull();
    }
}