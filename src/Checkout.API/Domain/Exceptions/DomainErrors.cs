namespace Checkout.API.Domain.Exceptions;

public static class DomainErrors
{
    public static class Order
    {
        public const string InvalidAmount = "ERR_ORDER_INVALID_AMOUNT";
        public const string NotFound = "ERR_ORDER_NOT_FOUND";
        public const string AlreadyPaid = "ERR_ORDER_ALREADY_PAID";
    }

    public static class Customer
    {
        public const string InvalidEmail = "ERR_CUSTOMER_INVALID_EMAIL";
    }
}