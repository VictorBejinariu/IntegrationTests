using Connector.Contracts;

namespace Connector.Interfaces;

public interface IPaymentService
{
    PaymentCreated Create(PaymentCreate paymentRequest);
}