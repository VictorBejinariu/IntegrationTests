using Microsoft.AspNetCore.Mvc;

namespace Cashier.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentsController:ControllerBase
{
    [HttpPost]
    [Route("create")]
    public ActionResult<CashierPaymentResponse> Create(CashierPaymentCreate request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result = new CashierPaymentResponse()
        {
            MerchantId = request.MerchantId,
            SiteId = request.SiteId,
            ClientUniqueId = request.ClientUniqueId
        };

        return Ok(result);
    }
}