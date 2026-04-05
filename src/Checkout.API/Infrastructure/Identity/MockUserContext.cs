using Checkout.API.Domain.Interfaces;

namespace Checkout.API.Infrastructure.Identity;

public class MockUserContext : IUserContext
{
    public Guid GetCurrentUserId() => Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851");

    public string GetCurrentUserEmail() => "jose.meurer@email.com";
}