using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Connector.Int.Tests;

public class HealthTests:IClassFixture<ConnectorApiFactory>
{
    private readonly HttpClient _client;

    public HealthTests(ConnectorApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            BaseAddress = new Uri ("https://localhost:7252")
        });
    }
    
    [Fact]
    public async Task Get_ShouldBeOk_Always()
    {
        //Given
        //Always
        
        //When
        var response = await _client.GetAsync("health");

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.OK);

    }
}