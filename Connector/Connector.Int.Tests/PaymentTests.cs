using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Connector.Contracts;
using Connector.Int.Tests.Utils;
using Connector.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Connector.Int.Tests;

public class PaymentTests:IClassFixture<ConnectorApiFactory>
{
    private readonly HttpClient _client;

    
    public PaymentTests(ConnectorApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            BaseAddress = new Uri ("https://localhost:7252")
        });
    }

    
    [Fact]
    public async Task Create_ShouldReturnValidationError_WhenAmountInvalid()
    {
        //Given
        var request = new PaymentCreate()
        {
            MerchantId = "1000",
            MerchantSiteId = "100000",
            Amount = "wrong amount",
            Currency = "Eur",
            CustomerFirstName = "Gigi",
            CustomerLastName = "Wrong"
        };
        
        //When
        var response = await _client.PostAsJsonAsync("payment",request);

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseContent = await response.Content.ReadAsStringAsync();

        responseContent.Should().Contain(Constants.ValidationErrorCode);
        responseContent.Should().Contain(Constants.ValidationErrorMessage);
    }
    
    [Fact]
    public async Task Create_ShouldBeOk_WhenAllOk()
    {
        //Given
        var request = new PaymentCreate()
        {
            MerchantId = "1000",
            MerchantSiteId = "100000",
            Amount = "100",
            Currency = "Eur",
            CustomerFirstName = "Gigi",
            CustomerLastName = "Wrong"
        };
        
        //When
        var response = await _client.PostAsJsonAsync("payment",request);

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}