using App.User.Entity;

namespace App.Web.Providers.Interfaces;

public interface ICurrentUserProvider
{
    bool IsLoggedIn();
    Task<AppUser> GetCurrentUser();
    long? GetCurrentUserId();
}