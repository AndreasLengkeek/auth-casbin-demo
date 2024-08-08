using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace auth_casbin.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    public AuthController()
    {
    }

    [HttpGet(nameof(GetWeatherForecast))]
    public IEnumerable<string> GetWeatherForecast()
    {
        return new List<string> { "65 C", "40 C"};
    }
}
