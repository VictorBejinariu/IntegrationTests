using Connector.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Connector.Int.Tests.Utils;

public class ConnectorApiFactory :WebApplicationFactory<IApiMarker>
{
    private readonly PaymentMockDb _mockDb = new();

    public PaymentMockDb GetMockDb()
    {
        return _mockDb;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });
        builder.ConfigureServices(c =>
        {
            c.AddSingleton<IPaymentRepository>(_mockDb);
        });
    }
}