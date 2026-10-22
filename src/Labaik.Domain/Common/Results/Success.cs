namespace Labaik.Domain.Common.Results;

/// <summary>Value of an operation that succeeds without returning data. The API maps it to 204 No Content.</summary>
public readonly record struct Success;

/// <summary>Shortcuts for building results.</summary>
public static class Result
{
    /// <summary>The single success value, used as: return Result.Success;</summary>
    public static Success Success => default;
}
