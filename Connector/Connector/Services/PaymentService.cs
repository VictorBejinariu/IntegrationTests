using Connector.Contracts;
using Connector.Interfaces;
using Connector.Models;

namespace Connector.Services;

public class PaymentService:IPaymentService
{
    private readonly ILogger<PaymentService> _logger;
    private readonly IPaymentRequestValidator _validator;
    private readonly IPaymentBuilder _builder;
    private readonly IPaymentRepository _repository;
    private readonly IHttpClientFactory _clientFactory;
    
    public PaymentService(
        ILogger<PaymentService> logger, 
        IPaymentRequestValidator validator, 
        IPaymentBuilder builder, 
        IPaymentRepository repository, IHttpClientFactory clientFactory)
    {
        _validator = validator??throw new ArgumentNullException(nameof(validator));
        _builder = builder??throw new ArgumentNullException(nameof(builder));
        _repository = repository??throw new ArgumentNullException(nameof(repository));
        _clientFactory = clientFactory??throw new ArgumentNullException(nameof(clientFactory));
        _logger = logger;
    }

    public async Task<PaymentCreated> Create(PaymentCreate paymentRequest)
    {
        var result = new PaymentCreated();
        result.IsSuccess = true;
        result.MerchantId = paymentRequest.MerchantId;
        result.SiteId = paymentRequest.MerchantSiteId;
        
        var errors = _validator.Validate(paymentRequest);
        if (errors.Any())
        {
            var reportedError = errors.First();
            result.ErrorCode = reportedError.ErrorCode;
            result.ErrorMessage = reportedError.ErrorMessage;
            result.IsSuccess = false;
            return result;
        }

        var payment = _builder.BuildFrm(paymentRequest);

        if(!await _repository.Insert(payment))
        {
            result.ErrorCode = Constants.GenericErrorCode;
            result.ErrorMessage = Constants.GenericErrorMessage;
            result.IsSuccess = false;
            return result;
        }

        // var httpClient = _clientFactory.CreateClient("Cashier");
        //
        // var cashierPaymentRequest = new CashierPaymentRequest()
        // {
        //     MerchantId = paymentRequest.MerchantId,
        //     SiteId = paymentRequest.MerchantSiteId,
        //     ClientUniqueId = payment.Id,
        //     CustomerFirstname = paymentRequest.CustomerFirstName,
        //     CustomerLastNane = paymentRequest.CustomerLastName
        // };
        //
        // var cashierPaymentResponse = await httpClient.PostAsJsonAsync("payments/create", cashierPaymentRequest);
        //
        // if (!cashierPaymentResponse.IsSuccessStatusCode)
        // {
        //     result.ErrorCode = Constants.ProviderErrorCode;
        //     result.ErrorMessage = Constants.ProviderErrorMessage;
        //     result.IsSuccess = false;
        //     return result;
        // }
        //
        //do more
        return result;
    }
}