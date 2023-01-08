﻿using Connector.Contracts;
using Connector.Interfaces;
using Connector.Models;

namespace Connector.Services;

public class PaymentService:IPaymentService
{
    private readonly ILogger<PaymentService> _logger;
    private readonly IPaymentRequestValidator _validator;
    private readonly IPaymentBuilder _builder;
    private readonly IPaymentRepository _repository;
    
    public PaymentService(
        ILogger<PaymentService> logger, 
        IPaymentRequestValidator validator, 
        IPaymentBuilder builder, 
        IPaymentRepository repository)
    {
        _validator = validator??throw new ArgumentNullException(nameof(validator));
        _builder = builder??throw new ArgumentNullException(nameof(builder));
        _repository = repository??throw new ArgumentNullException(nameof(repository));
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
        //do more
        return result;
    }
}