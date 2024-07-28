using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using WhatsUp.Aggregator;

namespace WhatsUp.Tests.IntegrationTests.IndividualCalls;

public class CatsApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact(DisplayName = "Cat facts are received from the Cats API.")]
    public async Task GetCatFactsAsync()
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "/catfact");

        // Act
        var response = await client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        result.Should().NotBeNull();
    }
}