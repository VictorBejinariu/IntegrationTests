using Connector.Entities;
using Connector.Interfaces;

namespace Connector.Services;

public class PaymentMsSqlRepository : IPaymentRepository
{
    private static ICollection<Payment> payments = new List<Payment>();
    public Task<bool> Insert(Payment payment)
    {
        payments.Add(payment);
        return Task.FromResult(true);
    }

    public Task<Payment> Get(string id)
    {
        return Task.FromResult(payments.FirstOrDefault(p => p.Id == id));
    }
}