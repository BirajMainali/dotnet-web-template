using App.Base.Constants;
using App.Base.Extensions;
using App.Base.Settings;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace App.User.Manager;

public class MultiTenantClaimManager : IMultiTenantClaimManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDataProtectionProvider _dataProtector;
    private readonly IOptions<AppSettings> _options;

    private readonly string _extraClaimsKey = AuthenticationKeyConstants.MultiTenantAuthenticationKey;

    public MultiTenantClaimManager(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtector, IOptions<AppSettings> options)
    {
        _httpContextAccessor = httpContextAccessor;
        _dataProtector = dataProtector;
        _options = options;
    }

    public string GetProtectedClaim(string key)
    {
        var protector = _dataProtector.CreateProtector(ResolvePurpose(_extraClaimsKey));
        var protectedClaims = protector.Protect(key);
        return protectedClaims;
    }

    public string GetMultiTenantConnectionKey()
    {
        var value = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == _extraClaimsKey);
        if (value == null) return null;
        var protector = _dataProtector.CreateProtector(ResolvePurpose(_extraClaimsKey));
        return protector.Unprotect(value.Value);
    }

    public void RemoveClaims()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(_extraClaimsKey);
    }

    private string ResolvePurpose(string? purpose)
        => purpose ?? _options.Value.DefaultDataProtectionPurpose ?? "damn-data-protection-purpose";
}