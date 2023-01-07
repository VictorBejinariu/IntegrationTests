﻿using Microsoft.AspNetCore.Mvc;

namespace Connector.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController:ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}