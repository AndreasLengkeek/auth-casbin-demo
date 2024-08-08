using auth_casbin;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.AddWebAPI();

var app = builder.Build();

await app.UseWebAPI();

app.Run();
