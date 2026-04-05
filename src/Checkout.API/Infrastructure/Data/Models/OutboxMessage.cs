namespace Checkout.API.Infrastructure.Data.Models;

public class OutboxMessage
{
    public Guid Id { get; init; }
    public string Type { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? ProcessedAt { get; private set; }
    public string? ErrorMessage { get; private set; }

    private OutboxMessage() { }

    public OutboxMessage(string type, string content)
    {
        Id = Guid.NewGuid();
        Type = type;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsProcessed()
    {
        ProcessedAt =  DateTime.UtcNow;
    }

    public void MarkAsFailed(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}