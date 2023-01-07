using Microsoft.AspNetCore.Mvc;

namespace Connector.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController:ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger??throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation($"{nameof(HealthController)}:{nameof(Get)}");
        return Ok();
    }
}