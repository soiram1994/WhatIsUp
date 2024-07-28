using Ocelot.Middleware;
using WhatsUp.Aggregator;
using WhatsUp.Aggregator.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services
    .RegisterOcelot(builder.Configuration)
    .RegisterServices()
    .RegisterRepos()
    .AddEndpointsApiExplorer()
    .AddControllers();
Environment.SetEnvironmentVariable("NEWS_API_KEY", "bf65b36a8b2f4b8fa5a6ae23c549db44");
Environment.SetEnvironmentVariable("WEATHER_API_KEY", "ede4feae499af9d350f53d3e2edf4691");

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();
app.UseMiddleware<ResponseTimeMiddleware>();
await app.UseOcelot();

app.Run();

public partial class Program
{
}