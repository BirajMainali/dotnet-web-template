using System.Security.Claims;
using App.Base.Constants;
using App.Base.Repository;
using App.Base.Settings;
using App.User.Entity;
using App.Web.Providers.Interfaces;
using Microsoft.Extensions.Options;

namespace App.Web.Providers;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IRepository<AppUser, long> _userRepository;
    private readonly IOptions<AppSettings> _options;

    public CurrentUserProvider(IHttpContextAccessor contextAccessor, IRepository<AppUser, long> userRepository, IOptions<AppSettings> options)
    {
        _contextAccessor = contextAccessor;
        _userRepository = userRepository;
        _options = options;
    }

    public bool IsLoggedIn() => GetCurrentUserId() != null;

    public async Task<AppUser> GetCurrentUser()
    {
        var userId = GetCurrentUserId();
        if (userId.HasValue) return await _userRepository.FindOrThrowAsync(userId.Value);
        return null;
    }

    public long? GetCurrentUserId()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(AuthenticationKeyConstants.AuthenticationKey);
        if (string.IsNullOrWhiteSpace(userId)) return null;
        return Convert.ToInt64(userId);
    }

    public string GetCurrentConnectionKey()
    {
        var connectionKey = _contextAccessor.HttpContext?.User.FindFirstValue(AuthenticationKeyConstants.MultiTenantAuthenticationKey);
        if (string.IsNullOrWhiteSpace(connectionKey)) return string.Empty;
        return connectionKey;
    }
}