using auth_casbin;

var builder = WebApplication.CreateBuilder(args);
builder.AddWebAPI();

var app = builder.Build();

app.UseWebAPI();

app.Run();
