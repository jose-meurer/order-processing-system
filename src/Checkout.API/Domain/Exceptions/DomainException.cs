namespace Checkout.API.Domain.Exceptions;

public class DomainException(string message, string? errorCode = null, int statusCode = 400)
    : Exception(message)
{
    public string? ErrorCode { get;} = errorCode;
    public int StatusCode { get; } = statusCode;
}