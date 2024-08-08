using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using auth_casbin.Data;
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
    }

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("database"));
        var dataSource = dataSourceBuilder.Build();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(dataSource));
    }

    public static async Task UseWebAPI(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();

            using var scope = app.Services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
        }

        app.Use(async (context, next) => {
            app.Logger.LogInformation("Logging");
            await next(context);
        });

        app.MapControllers();

    }
}
