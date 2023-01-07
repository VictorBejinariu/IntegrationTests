using Connector.Contracts;
using Connector.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Connector.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService ?? throw new ArgumentNullException();
    }

    [HttpPost]
    public ActionResult<PaymentCreated> CreatePayment(PaymentCreate payment)
    {
        var result = _paymentService.Create(payment);
                
        return Ok(result);
    }

}