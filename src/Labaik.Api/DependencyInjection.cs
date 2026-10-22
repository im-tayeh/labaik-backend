using Labaik.Api.Endpoints;
using Labaik.Api.Options;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Labaik.Api;

/// <summary>Registers and wires the HTTP layer.</summary>
internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services
            .AddOptions<SeqOptions>()
            .BindConfiguration(SeqOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<OpenApiDocsOptions>()
            .BindConfiguration(OpenApiDocsOptions.SectionName);

        services.AddOpenApi(OpenApiDocsOptions.DocumentName, options => options.AddDocumentTransformer((document, _, _) =>
        {
            document.Info ??= new();
            document.Info.Title = "Labaik API";
            document.Info.Version = "v1";
            document.Info.Description = "Backend for the Labaik Umrah companion app. All endpoints live under /api/v1.";
            return Task.CompletedTask;
        }));

        services.AddSerilog(ConfigureSerilog);

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options => options.GetLevel = GetRequestLogLevel);

        if (app.Services.GetRequiredService<IOptions<OpenApiDocsOptions>>().Value.Enabled)
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options => options.WithTitle("Labaik API"));
        }

        app.MapApiV1();

        return app;
    }

    private static void ConfigureSerilog(IServiceProvider serviceProvider, LoggerConfiguration logger)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
        var seq = serviceProvider.GetRequiredService<IOptions<SeqOptions>>().Value;

        logger
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(serviceProvider)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "Labaik.Api");

        if (environment.IsDevelopment())
        {
            logger.WriteTo.Console();
        }
        else
        {
            logger.WriteTo.Console(new RenderedCompactJsonFormatter());
        }

        if (!string.IsNullOrWhiteSpace(seq.ServerUrl))
        {
            logger.WriteTo.Seq(seq.ServerUrl);
        }
    }

    private static LogEventLevel GetRequestLogLevel(HttpContext context, double elapsedMilliseconds, Exception? exception) =>
        exception is not null || context.Response.StatusCode >= StatusCodes.Status500InternalServerError
            ? LogEventLevel.Error
            : LogEventLevel.Information;
}
