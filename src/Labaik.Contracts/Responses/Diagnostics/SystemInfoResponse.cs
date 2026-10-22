namespace Labaik.Contracts.Responses.Diagnostics;

/// <summary>Identifies the backend build a client is talking to.</summary>
/// <param name="Service">Service name, always "labaik-api".</param>
/// <param name="Version">Semantic version of the build.</param>
/// <param name="Environment">Hosting environment: Development, Testing or Production.</param>
/// <param name="Commit">Git commit SHA of the build, or "local".</param>
public sealed record SystemInfoResponse(string Service, string Version, string Environment, string Commit);
