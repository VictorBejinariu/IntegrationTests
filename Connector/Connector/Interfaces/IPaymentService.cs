using Connector.Contracts;

namespace Connector.Interfaces;

public interface IPaymentService
{
    Task<PaymentCreated> Create(PaymentCreate paymentRequest);
}
