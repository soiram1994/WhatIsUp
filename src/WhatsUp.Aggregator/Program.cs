using Ocelot.Middleware;
using WhatsUp.Aggregator;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services
    .ConfigureOcelot(builder.Configuration)
    .ConfigureServices()
    .AddEndpointsApiExplorer();
Environment.SetEnvironmentVariable("NEWS_API_KEY", "bf65b36a8b2f4b8fa5a6ae23c549db44");
Environment.SetEnvironmentVariable("WEATHER_API_KEY", "ede4feae499af9d350f53d3e2edf4691");

var app = builder.Build();


app.UseHttpsRedirection();
await app.UseOcelot();

app.Run();

public partial class Program
{
}