using Checkout.API.Application.Dtos;
using Checkout.API.Application.Interfaces;
using Checkout.API.Domain.Entities;
using Checkout.API.Domain.Exceptions;
using Checkout.API.Domain.Interfaces;
using Shared.Integration.Events;

namespace Checkout.API.Application.Commands;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository, 
    IUserContext userContext,
    IEventBus eventBus)
{
    public async Task<Guid> Handle(CreateOrderRequestDto request, CancellationToken cancellationToken)
    {
        if (request.TotalAmount <= 0)
            throw new DomainException("O valor do pedido deve ser maior que zero.", DomainErrors.Order.InvalidAmount);

        var customerId = userContext.GetCurrentUserId();
        var customerEmail = userContext.GetCurrentUserEmail();
        
        //TODO:Checar estoque

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
        
        await eventBus.PublishAsync(orderPlacedEvent, cancellationToken);
        await orderRepository.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}