using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Connector.Contracts;
using Connector.Int.Tests.TestModels;
using Connector.Int.Tests.Utils;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Connector.Int.Tests;

public class PaymentFrameworkTests:IClassFixture<ConnectorApiFactory>
{
    private readonly HttpClient _client;
    private readonly PaymentMockDb _mockDb;

        
    public PaymentFrameworkTests(ConnectorApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            BaseAddress = new Uri ("https://localhost:7252")
        });
        _mockDb = apiFactory.GetMockDb();
    }

    
    /// <summary>
    /// In this way we have a test per 
    /// </summary>
    /// <param name="data"></param>
    [Theory]
    [ClassData(typeof(PaymentAllTestData))]
    public async Task AllTests(PaymentTest data)
    {
        //When
        var response = await _client.PostAsJsonAsync("payment",data.Request);

        if (data.Response != null)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var paymentCreated = JsonConvert.DeserializeObject<PaymentCreated>(responseContent);

            paymentCreated.Should().NotBeNull();
            paymentCreated.MerchantId.Should().Be(data.Response.MerchantId);
            paymentCreated.SiteId.Should().Be(data.Response.SiteId);
            paymentCreated.ErrorCode.Should().Be(data.Response.ErrorCode);
            paymentCreated.ErrorMessage.Should().Be(data.Response.ErrorMessage);

        }

        if (data.Db != null)
        {
            var payment = _mockDb.Data.FirstOrDefault(p => p.MerchantPaymentId == data.Request.MerchantTransactionId);
            payment.Should().NotBeNull(because: "Payment Should be in db");

            payment.MerchantId.Should().Be(data.Db.MerchantId);
            payment.SiteId.Should().Be(data.Db.SiteId);
        }
    }
}