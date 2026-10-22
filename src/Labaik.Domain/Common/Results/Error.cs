namespace Labaik.Domain.Common.Results;

/// <summary>
/// An expected failure with a stable machine-readable code such as "Group.Full".
/// Clients translate errors by <see cref="Code"/>, so a released code never changes.
/// </summary>
/// <param name="Code">Stable code in the form Area.Reason.</param>
/// <param name="Description">Developer-facing English description.</param>
/// <param name="Kind">Category that decides the HTTP status code.</param>
public sealed record Error(string Code, string Description, ErrorKind Kind)
{
    /// <summary>JSON path of the invalid field. Set only for <see cref="ErrorKind.InvalidRequest"/>.</summary>
    public string? Field { get; init; }

    public static Error Failure(string code, string description) => new(code, description, ErrorKind.Failure);

    public static Error Validation(string code, string description) => new(code, description, ErrorKind.Validation);

    public static Error InvalidRequest(string field, string code, string description) =>
        new(code, description, ErrorKind.InvalidRequest) { Field = field };

    public static Error NotFound(string code, string description) => new(code, description, ErrorKind.NotFound);

    public static Error Conflict(string code, string description) => new(code, description, ErrorKind.Conflict);

    public static Error Unauthorized(string code, string description) => new(code, description, ErrorKind.Unauthorized);

    public static Error Forbidden(string code, string description) => new(code, description, ErrorKind.Forbidden);

    public static Error TooManyRequests(string code, string description) => new(code, description, ErrorKind.TooManyRequests);

    public static Error Unexpected(string code, string description) => new(code, description, ErrorKind.Unexpected);
}
