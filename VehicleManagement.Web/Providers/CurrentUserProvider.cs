using System.Security.Claims;
using VehicleManagement.User.Repositories.Interfaces;
using VehicleManagement.Web.Providers.Interfaces;

namespace VehicleManagement.Web.Providers;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserRepository _userRepository;

    public CurrentUserProvider(IHttpContextAccessor contextAccessor, IUserRepository userRepository)
    {
        _contextAccessor = contextAccessor;
        _userRepository = userRepository;
    }

    public bool IsLoggedIn() 
        => GetCurrentUserId() != null;

    public async Task<User.Model.User> GetCurrentUser()
    {
        var userId = GetCurrentUserId();
        if (userId.HasValue) return await _userRepository.FindOrThrowAsync(userId.Value);
        return null;
    }

    public long? GetCurrentUserId()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue("Id");
        if (string.IsNullOrWhiteSpace(userId)) return null;
        return Convert.ToInt64(userId);
    }
}