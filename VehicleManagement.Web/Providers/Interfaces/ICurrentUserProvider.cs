namespace VehicleManagement.Web.Providers.Interfaces;

public interface ICurrentUserProvider
{
    bool IsLoggedIn();
    Task<User.Model.User> GetCurrentUser();
    long? GetCurrentUserId();
}