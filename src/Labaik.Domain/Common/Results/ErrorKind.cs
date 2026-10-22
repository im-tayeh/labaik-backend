namespace Labaik.Domain.Common.Results;

/// <summary>
/// Category of an expected failure. The API layer maps each kind to an HTTP status code.
/// Values are part of the contract with clients: append new members, never renumber.
/// </summary>
public enum ErrorKind
{
    /// <summary>A business rule was violated (400).</summary>
    Failure = 0,

    /// <summary>A domain value is invalid, for example a malformed phone number (400).</summary>
    Validation = 1,

    /// <summary>The request shape is invalid and the error carries the field path (422).</summary>
    InvalidRequest = 2,

    /// <summary>The resource does not exist or is not visible to the caller (404).</summary>
    NotFound = 3,

    /// <summary>The request conflicts with the current state (409).</summary>
    Conflict = 4,

    /// <summary>The caller is not authenticated (401).</summary>
    Unauthorized = 5,

    /// <summary>The caller is authenticated but not allowed (403).</summary>
    Forbidden = 6,

    /// <summary>The caller exceeded a rate limit (429).</summary>
    TooManyRequests = 7,

    /// <summary>An unexpected failure (500).</summary>
    Unexpected = 8,
}
