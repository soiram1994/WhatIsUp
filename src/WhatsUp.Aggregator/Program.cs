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

var app = builder.Build();


app.UseHttpsRedirection();
await app.UseOcelot();

app.Run();

public partial class Program
{
}