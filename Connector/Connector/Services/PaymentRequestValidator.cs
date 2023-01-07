using Connector.Contracts;
using Connector.Interfaces;
using Connector.Models;

namespace Connector.Services;

public class PaymentRequestValidator:IPaymentRequestValidator
{
    public ICollection<Error> Validate(PaymentCreate input)
    {
        var result = new List<Error>();
        if (!decimal.TryParse(input.Amount, out _))
        {
            result.Add(new Error()
            {
                ErrorCode = Constants.ValidationErrorCode,
                ErrorMessage = Constants.ValidationErrorMessage
            });
        }

        return result;
    }
}