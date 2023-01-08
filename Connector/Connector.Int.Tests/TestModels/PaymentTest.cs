using Connector.Contracts;
using Connector.Entities;

namespace Connector.Int.Tests.TestModels;

public class PaymentTest
{
    public PaymentCreate Request { get; set; }
    public PaymentCreated Response { get; set; }
    public Payment Db { get; set; }
}