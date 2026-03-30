namespace Checkout.API.Domain.Interfaces;

public interface IUserContext
{
    Guid GetCurrentUserId();
    
    string GetCurrentUserEmail();
}