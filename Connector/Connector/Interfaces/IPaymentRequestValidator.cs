using Connector.Contracts;
using Connector.Models;

namespace Connector.Interfaces;

public interface IPaymentRequestValidator
{
    ICollection<Error> Validate(PaymentCreate input);
}