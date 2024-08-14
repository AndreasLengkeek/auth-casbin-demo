using System.Security.Claims;
using auth_casbin.Data;
using auth_casbin.Models;
using Casbin.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace auth_casbin.Controllers;

[CasbinAuthorize]
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class AuthController(ApplicationDbContext context, ILogger<AuthController> logger) : ControllerBase
{
    [CasbinAuthorize]
    [HttpGet(nameof(GetWeatherForecast))]
    public async Task<IEnumerable<Forecast>> GetWeatherForecast()
    {
        logger.LogInformation($"Start {nameof(GetWeatherForecast)}");
        var results = await context.Forecasts.ToListAsync();
        return results;
    }

    [AllowAnonymous]
    [HttpGet(nameof(GetUser))]
    public JsonResult GetUser()
    {
        var name = HttpContext.User.FindFirstValue(ClaimTypes.Name);
        var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
        return new JsonResult(new {
            Name = name ?? "No user",
            Role = role
        });
    }

    [CasbinAuthorize()]
    [HttpGet(nameof(GetData))]
    public async Task<IEnumerable<Forecast>> GetData()
    {
        logger.LogInformation($"Start {nameof(GetWeatherForecast)}");
        var results = await context.Forecasts.ToListAsync();
        return results;
    }

    [AllowAnonymous]
    [HttpGet(nameof(Login))]
    public async Task<string> Login(string username)
    {
        var user = new {
            Name = username
        };

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, "admin"),
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        logger.LogInformation($"User {user.Name} logged in");

        return user.Name;
    }

    [AllowAnonymous]
    [HttpGet(nameof(LogOut))]
    public async Task<string> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return "Ok";
    }
}
