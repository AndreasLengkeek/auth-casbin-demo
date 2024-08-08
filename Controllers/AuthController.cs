using auth_casbin.Data;
using auth_casbin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace auth_casbin.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class AuthController(ApplicationDbContext context, ILogger<AuthController> logger) : ControllerBase
{
    [HttpGet(nameof(GetWeatherForecast))]
    public async Task<IEnumerable<Forecast>> GetWeatherForecast()
    {
        logger.LogInformation($"Start {nameof(GetWeatherForecast)}");
        var results = await context.Forecasts.ToListAsync();
        return results;
    }

    [HttpGet(nameof(Sample))]
    public string Sample()
    {
        return "Ok";
    }
}
