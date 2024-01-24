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

    private readonly string _extraClaimsKey = ClaimsConstants.ProtectedClaimsKey;

    public MultiTenantClaimManager(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtector, IOptions<AppSettings> options)
    {
        _httpContextAccessor = httpContextAccessor;
        _dataProtector = dataProtector;
        _options = options;
    }

    public string SaveClaims(Dictionary<string, string> claims)
    {
        var claimsSerialized = claims.ToJson();
        var protector = _dataProtector.CreateProtector(ResolvePurpose(_extraClaimsKey));
        var protectedClaims = protector.Protect(claimsSerialized);
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(_extraClaimsKey, protectedClaims, new()
        {
            Expires = DateTimeOffset.Now.AddDays(2),
            HttpOnly = true,
            IsEssential = true
        });
        return protectedClaims;
    }

    public string GetClaimsFromRequest()
    {
        string value;
        if (!_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(_extraClaimsKey, out value))
        {
            StringValues header;
            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(_extraClaimsKey, out header))
            {
                value = header.FirstOrDefault();
            }
        }

        if (value.ValueOrNull() == null)
        {
            return null;
        }

        var protector = _dataProtector.CreateProtector(ResolvePurpose(_extraClaimsKey));
        return protector.Unprotect(value);
    }

    public void RemoveClaims()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete(_extraClaimsKey);
    }

    private string ResolvePurpose(string? purpose)
        => purpose ?? _options.Value.DefaultDataProtectionPurpose ?? "damn-data-protection-purpose";
}