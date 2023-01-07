using Connector.Contracts;
using Connector.Entities;

namespace Connector.Interfaces;

public interface IPaymentBuilder
{
    Payment BuildFrm(PaymentCreate input);
}