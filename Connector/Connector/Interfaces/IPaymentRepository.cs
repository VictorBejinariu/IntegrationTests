using Connector.Entities;

namespace Connector.Interfaces;

public interface IPaymentRepository
{
    Task<bool> Insert(Payment payment);
    Task<Payment> Get(string Id);
}