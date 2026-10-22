using Labaik.Api;
using Labaik.Application;
using Labaik.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Labaik API");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddPresentation(builder.Configuration)
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    app.UsePresentation();

    await app.RunAsync();
}
catch (Exception exception) when (exception is not HostAbortedException)
{
    Log.Fatal(exception, "Labaik API terminated unexpectedly");
    Environment.ExitCode = 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}

/// <summary>Exposes the entry point type to WebApplicationFactory in integration tests.</summary>
public partial class Program;
