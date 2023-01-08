using System;
using System.Linq;
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
    private readonly PaymentMockDb _mockDb;

    
    public PaymentTests(ConnectorApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            BaseAddress = new Uri ("https://localhost:7252")
        });
        _mockDb = apiFactory.GetMockDb();
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
    
    [Fact(Skip = "Integration Tests should not have mocked behaviour(like Db)")]
    public async Task Create_ReturnsResponseWithErrorCode_WhenDbFails()
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
        
        //This should not happen in Integration Tests
        _mockDb.SetupInsert(p => Task.FromResult(false));
        
        //When
        var response = await _client.PostAsJsonAsync("payment",request);

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        
        responseContent.Should().Contain(Constants.GenericErrorCode);
        responseContent.Should().Contain(Constants.GenericErrorMessage);
    }
    
    [Fact]
    public async Task Create_HavePaymentInDb_WhenAllOk()
    {
        //Given
        var request = new PaymentCreate()
        {
            MerchantId = "1000",
            MerchantSiteId = "100000",
            MerchantTransactionId = "SomeMerchantTransactionId",
            Amount = "100",
            Currency = "Eur",
            CustomerFirstName = "Gigi",
            CustomerLastName = "Wrong"
        };
        
        //When
        var response = await _client.PostAsJsonAsync("payment",request);

        //Then
        var dbPayment = _mockDb.Data.FirstOrDefault(p => p.MerchantPaymentId == request.MerchantTransactionId);

        dbPayment.Should().NotBeNull();
        dbPayment.MerchantId.Should().Be(request.MerchantId);
        dbPayment.SiteId.Should().Be(request.MerchantSiteId);
    }
    
}