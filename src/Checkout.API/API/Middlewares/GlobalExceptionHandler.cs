using Checkout.API.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.API.API.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problem = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };

        if (exception is DomainException domainException)
        {
            problem.Status = domainException.StatusCode;
            problem.Title = "Regra de Negócio Violada";
            problem.Detail = domainException.Message;
            problem.Extensions["errorCode"] = domainException.ErrorCode;
        }
        else
        {
            problem.Status = StatusCodes.Status500InternalServerError;
            problem.Title = "Erro Interno no Servidor";
            problem.Detail = "Ocorreu um erro inesperado. Tente novamente mais tarde.";
        }

        httpContext.Response.StatusCode = problem.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;
    }
}