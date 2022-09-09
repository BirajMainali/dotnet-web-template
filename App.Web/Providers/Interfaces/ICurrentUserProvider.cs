namespace App.Web.Providers.Interfaces;

public interface ICurrentUserProvider
{
    bool IsLoggedIn();
    Task<App.User.Model.User> GetCurrentUser();
    long? GetCurrentUserId();
}