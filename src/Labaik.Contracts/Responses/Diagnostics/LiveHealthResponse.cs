namespace Labaik.Contracts.Responses.Diagnostics;

/// <summary>Liveness probe body. The CD pipeline reads <see cref="Commit"/> to confirm which build is running.</summary>
/// <param name="Status">Always "Healthy" when the process can answer.</param>
/// <param name="Commit">Git commit SHA of the running build, or "local".</param>
public sealed record LiveHealthResponse(string Status, string Commit);
