using Connector.Contracts;
using Connector.Entities;
using Connector.Interfaces;

namespace Connector.Services;

public class PaymentBuilder : IPaymentBuilder
{
    private readonly IGuidProvider _guidProvider;

    public PaymentBuilder(IGuidProvider guidProvider)
    {
        _guidProvider = guidProvider??throw new ArgumentNullException(nameof(guidProvider));
    }

    public Payment BuildFrm(PaymentCreate input)
    {
        return new Payment()
        {
            Id = _guidProvider.NewGuid().ToString(),
            MerchantPaymentId = input.MerchantTransactionId,
            MerchantId = input.MerchantId,
            SiteId = input.MerchantSiteId
        };
    }
}