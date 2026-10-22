namespace Labaik.Domain.Common.Results;

/// <summary>
/// Outcome of an operation: either a value or one or more expected errors.
/// Expected failures are returned, not thrown. Exceptions are reserved for bugs and infrastructure faults.
/// </summary>
/// <typeparam name="TValue">Type of the value on success.</typeparam>
public sealed class Result<TValue>
{
    private static readonly IReadOnlyList<Error> NoErrors = [];

    private readonly TValue? _value;
    private readonly IReadOnlyList<Error>? _errors;

    private Result(TValue value) => _value = value;

    private Result(IReadOnlyList<Error> errors) => _errors = errors;

    public bool IsSuccess => _errors is null;

    public bool IsError => _errors is not null;

    /// <summary>The value. Throws when the result is an error, so check <see cref="IsSuccess"/> first.</summary>
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot read the value of a failed result.");

    public IReadOnlyList<Error> Errors => _errors ?? NoErrors;

    public Error FirstError => _errors is { Count: > 0 } errors
        ? errors[0]
        : throw new InvalidOperationException("A successful result has no errors.");

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static implicit operator Result<TValue>(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return new([error]);
    }

    public static implicit operator Result<TValue>(List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        return errors.Count > 0
            ? new(errors.ToArray())
            : throw new ArgumentException("At least one error is required.", nameof(errors));
    }

    /// <summary>Runs exactly one of the two functions, depending on the outcome.</summary>
    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<IReadOnlyList<Error>, TResult> onError)
    {
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onError);

        return _errors is null ? onSuccess(_value!) : onError(_errors);
    }
}
