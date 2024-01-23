using System.Security.Claims;
using App.Base.Repository;
using App.User.Entity;
using App.Web.Providers.Interfaces;

namespace App.Web.Providers;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IRepository<AppUser, long> _userRepository;

    public CurrentUserProvider(IHttpContextAccessor contextAccessor, IRepository<AppUser, long> userRepository)
    {
        _contextAccessor = contextAccessor;
        _userRepository = userRepository;
    }

    public bool IsLoggedIn() => GetCurrentUserId() != null;

    public async Task<AppUser> GetCurrentUser()
    {
        var userId = GetCurrentUserId();
        if (userId.HasValue) return await _userRepository.FindOrThrowAsync(userId.Value, "User not found");
        return null;
    }

    public long? GetCurrentUserId()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue("Id");
        if (string.IsNullOrWhiteSpace(userId)) return null;
        return Convert.ToInt64(userId);
    }
}