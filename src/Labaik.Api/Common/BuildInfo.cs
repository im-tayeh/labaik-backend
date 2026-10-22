using System.Reflection;

namespace Labaik.Api.Common;

/// <summary>Build metadata of the running process.</summary>
internal static class BuildInfo
{
    /// <summary>Informational version, for example 0.1.0 or 0.1.0+3f2a9c1.</summary>
    public static string Version { get; } =
        typeof(BuildInfo).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "0.0.0";

    /// <summary>Git commit injected by Render (RENDER_GIT_COMMIT), or "local" outside Render.</summary>
    public static string Commit(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        return configuration["RENDER_GIT_COMMIT"] is { Length: > 0 } commit ? commit : "local";
    }
}
