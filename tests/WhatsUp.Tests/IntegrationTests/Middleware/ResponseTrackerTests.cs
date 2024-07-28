using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using WhatsUp.Aggregator.DTOs;

namespace WhatsUp.Tests.IntegrationTests.Middleware;

public class ResponseTrackerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact(DisplayName = "Tracker is called when making a request.")]
    public async Task TrackerIsCalledAsync()
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/whatsup/London");

        // Act
        var response = await client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        result.Should().NotBeNull();

        // Act
        var statsRequest = new HttpRequestMessage(HttpMethod.Get, "/api/Stats/whatsup/");
        var statsResponse = await client.SendAsync(statsRequest);

        // Assert
        statsResponse.EnsureSuccessStatusCode();
        var statsResult = await statsResponse.Content.ReadFromJsonAsync<IEnumerable<ResponseTrackerDTO>>();
        statsResult.Should().NotBeNull();
        statsResult.Should().NotBeEmpty();
        statsResult.Should().Contain(d => d.RequestPath == "whatsup");
    }
}