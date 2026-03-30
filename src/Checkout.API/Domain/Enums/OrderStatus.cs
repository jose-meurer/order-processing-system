namespace Checkout.API.Domain.Enums;

public enum OrderStatus
{
    Pending = 1,
    Validated = 2,
    Reject = 3,
    Paid = 4,
    PaymentFailed = 5,
    Cancelled = 6
}