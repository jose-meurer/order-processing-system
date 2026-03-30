namespace Checkout.API.Application.Dtos;

public class CreateOrderRequestDto
{
    public string? CartId { get; set; }
    public decimal TotalAmount { get; set; }
}