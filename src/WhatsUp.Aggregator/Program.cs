using Ocelot.Middleware;
using WhatsUp.Aggregator;
using WhatsUp.Aggregator.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile("ocelot.json", false, true)
    .AddEnvironmentVariables();
builder.Services
    .RegisterOcelot(builder.Configuration)
    .RegisterServices()
    .RegisterRepos()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();
app.UseMiddleware<ResponseTimeMiddleware>();
await app.UseOcelot();

app.Run();

public partial class Program
{
}