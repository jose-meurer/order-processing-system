using Checkout.API.Domain.Enums;

namespace Checkout.API.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public string CustomerEmail { get; private set; } = string.Empty;
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Order() { }
    
    public Order(Guid customerId, string customerEmail, decimal totalAmount)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        CustomerEmail = customerEmail;
        TotalAmount = totalAmount;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void MarkAsValidated() => Status = OrderStatus.Validated;
    public void Reject() =>  Status = OrderStatus.Reject;
    public void MarkAsPaid() => Status = OrderStatus.Paid;
    public void FailPayment() => Status = OrderStatus.PaymentFailed;
    public void Cancel() => Status = OrderStatus.Cancelled;
}