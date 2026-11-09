using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FraudEngine.Api.ErrorHandling;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, detail) = exception switch
        {
            ValidationException validationException =>
                (StatusCodes.Status400BadRequest,
                    "Validation error",
                    string.Join(" ", validationException.Errors.Select(error => error.ErrorMessage))),
            DbUpdateException =>
                (StatusCodes.Status409Conflict,
                    "Transaction conflict",
                    "The transaction could not be persisted because it conflicts with an existing transaction."),
            _ =>
                (StatusCodes.Status500InternalServerError,
                    "Unexpected error",
                    "An unexpected error occurred while processing the request.")
        };

        if (statusCode >= 500)
        {
            logger.LogError(exception, "Unhandled exception while processing request");
        }
        else
        {
            logger.LogWarning(exception, "Request failed with status code {StatusCode}", statusCode);
        }

        httpContext.Response.StatusCode = statusCode;

        await Results.Problem(
            statusCode: statusCode,
            title: title,
            detail: detail,
            extensions: new Dictionary<string, object?>
            {
                ["traceId"] = httpContext.TraceIdentifier
            })
            .ExecuteAsync(httpContext);

        return true;
    }
}
