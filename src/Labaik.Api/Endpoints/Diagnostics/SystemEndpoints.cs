using Labaik.Api.Common;
using Labaik.Contracts.Responses.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Labaik.Api.Endpoints.Diagnostics;

internal sealed class SystemEndpoints : IEndpointGroup
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/system").WithTags("System");

        group.MapGet("/info", GetInfo)
            .WithName("System.GetInfo");
    }

    /// <summary>Returns the version, environment and commit of the running API.</summary>
    /// <remarks>Flutter and the web dashboard call this to confirm which backend build they are using.</remarks>
    private static Ok<SystemInfoResponse> GetInfo(IHostEnvironment environment, IConfiguration configuration) =>
        TypedResults.Ok(new SystemInfoResponse(
            Service: "labaik-api",
            Version: BuildInfo.Version,
            Environment: environment.EnvironmentName,
            Commit: BuildInfo.Commit(configuration)));
}
