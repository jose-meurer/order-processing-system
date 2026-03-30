using Checkout.API.Application.Dtos;
using Checkout.API.Domain.Entities;
using Checkout.API.Domain.Interfaces;
using Shared.Integration.Events;

namespace Checkout.API.Application.Commands;

public class CreateOrderCommandHandler(IOrderRepository orderRepository, IUserContext userContext)
{
    public async Task<Guid> Handle(CreateOrderRequestDto request, CancellationToken cancellationToken)
    {
        if (request.TotalAmount <= 0)
            throw new ArgumentException("O valor do pedido deve ser maior que zero.");

        var customerId = userContext.GetCurrentUserId();
        var customerEmail = userContext.GetCurrentUserEmail();
        
        //Checar estoque no Redis

        var order = new Order(customerId, customerEmail, request.TotalAmount);
        await orderRepository.AddAsync(order, cancellationToken);

        var orderPlacedEvent = new OrderPlacedEvent()
        {
            OrderId = order.Id,
            CustomerId = customerId,
            CustomerEmail = customerEmail,
            TotalAmount = request.TotalAmount,
            CreatedAt = order.CreatedAt
        };
        
        //TODO: Adicionar o orderPlacedEvent no OutboxRepository
        await orderRepository.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}