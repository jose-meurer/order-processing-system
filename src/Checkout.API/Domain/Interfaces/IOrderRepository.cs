using Checkout.API.Domain.Entities;

namespace Checkout.API.Domain.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}