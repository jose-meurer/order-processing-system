using System.Text.Json;
using Checkout.API.Application.Interfaces;
using Checkout.API.Infrastructure.Data.Contexts;
using Checkout.API.Infrastructure.Data.Models;

namespace Checkout.API.Infrastructure.Messaging;

public class OutboxEventBus(AppDbContext context) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken) where TEvent : class
    {
        var eventType = nameof(integrationEvent);

        var payload = JsonSerializer.Serialize(integrationEvent);
        
        var outboxMessage = new OutboxMessage(eventType, payload);
        
        await context.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
    }
}