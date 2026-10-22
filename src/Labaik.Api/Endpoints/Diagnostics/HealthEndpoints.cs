using Labaik.Api.Common;
using Labaik.Contracts.Responses.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Labaik.Api.Endpoints.Diagnostics;

/// <summary>Health probes. They live outside /api/v1 and outside the OpenAPI document.</summary>
internal static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/health/live", GetLive)
            .ShortCircuit()
            .DisableHttpMetrics()
            .ExcludeFromDescription();

        app.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = check => check.Tags.Contains("ready") })
            .DisableHttpMetrics();

        return app;
    }

    private static Ok<LiveHealthResponse> GetLive(IConfiguration configuration) =>
        TypedResults.Ok(new LiveHealthResponse("Healthy", BuildInfo.Commit(configuration)));
}
