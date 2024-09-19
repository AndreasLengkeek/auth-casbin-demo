using System.Security.Claims;
using auth_casbin.Auth;
using auth_casbin.Common;
using auth_casbin.Data;
using Casbin;
using Casbin.AspNetCore.Authorization;
using Casbin.AspNetCore.Authorization.Transformers;
using Casbin.Model;
using Casbin.Persist.Adapter.EFCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace auth_casbin;

public static class ConfigureServices
{
    public static void AddWebAPI(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();
    }

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("database"));
        var dataSource = dataSourceBuilder.Build();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(dataSource));

        services.AddDbContext<CasbinDatabaseContext>(options => {
            options.UseNpgsql(configuration.GetConnectionString("casbin"));
        });
        services.AddSingleton<IRequestTransformer<StringRequestValues>, CustomRequestTransformer>();

        services.AddCasbinAuthorization(options =>
        {
            options.PreferSubClaimType = ClaimTypes.Name;

            options.DefaultModelPath = "CasbinConfig/rbac_model.conf";
            options.DefaultPolicyPath = "CasbinConfig/basic_policy.csv";

            // Load Policy from database instead of file
            // options.DefaultEnforcerFactory = (p, m) =>
            //     new Enforcer(m, new EFCoreAdapter<int>(p.GetRequiredService<CasbinDatabaseContext>()));

            options.DefaultRequestTransformerType = typeof(CustomRequestTransformer);
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns> <summary>
    ///
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static async Task UseWebAPI(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();

            // using var scope = app.Services.CreateScope();
            // await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
        }

        app.UseAuthentication();
        app.UseCasbinAuthorization();
        app.UseAuthorization();

        app.MapControllers();
    }
}
