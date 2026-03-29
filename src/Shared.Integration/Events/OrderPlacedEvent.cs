namespace Shared.Integration.Events;

/// <summary>
/// Evento disparado quando um cliente finaliza o checkout e o pedido é salvo no banco com sucesso.
/// </summary>
public class OrderPlacedEvent
{
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public string? CustomerEmail { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTime CreatedAt { get; init; }
}