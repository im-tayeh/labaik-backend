using Labaik.Domain.Common.Results;

namespace Labaik.Api.Common.ErrorHandling;

/// <summary>Converts application results into HTTP responses that follow the error contract with clients.</summary>
internal static class ResultExtensions
{
    public static IResult ToHttpResult<TValue>(this Result<TValue> result, Func<TValue, IResult> onSuccess)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.Match(onSuccess, ToProblem);
    }

    public static IResult ToHttpResult<TValue>(this Result<TValue> result) =>
        result.ToHttpResult(value => value is Success ? TypedResults.NoContent() : (IResult)TypedResults.Ok(value));

    private static IResult ToProblem(IReadOnlyList<Error> errors)
    {
        if (errors.All(error => error.Kind == ErrorKind.InvalidRequest))
        {
            var fieldErrors = errors
                .GroupBy(error => error.Field ?? string.Empty)
                .ToDictionary(group => group.Key, group => group.Select(error => error.Description).ToArray());

            var validationProblem = new HttpValidationProblemDetails(fieldErrors)
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Title = "One or more validation errors occurred.",
            };
            validationProblem.Extensions["code"] = "Request.Invalid";

            return TypedResults.Problem(validationProblem);
        }

        var error = errors[0];

        return TypedResults.Problem(
            statusCode: ToStatusCode(error.Kind),
            detail: error.Description,
            extensions: new Dictionary<string, object?> { ["code"] = error.Code });
    }

    private static int ToStatusCode(ErrorKind kind) => kind switch
    {
        ErrorKind.Failure or ErrorKind.Validation => StatusCodes.Status400BadRequest,
        ErrorKind.InvalidRequest => StatusCodes.Status422UnprocessableEntity,
        ErrorKind.NotFound => StatusCodes.Status404NotFound,
        ErrorKind.Conflict => StatusCodes.Status409Conflict,
        ErrorKind.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorKind.Forbidden => StatusCodes.Status403Forbidden,
        ErrorKind.TooManyRequests => StatusCodes.Status429TooManyRequests,
        _ => StatusCodes.Status500InternalServerError,
    };
}
