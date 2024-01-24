using App.Base.Constants;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace App.User.Provider;

public class ProtectedClaimProvider : IProtectedClaimProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private Dictionary<string, string> _protectedClaims;

    public ProtectedClaimProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Dictionary<string, string> GetProtectedClaims()
    {
        if (_protectedClaims != null) return _protectedClaims;
        object data;
        if (_httpContextAccessor.HttpContext == null || !_httpContextAccessor.HttpContext.Items.TryGetValue(ClaimsConstants.ProtectedClaimsHttpClaimKey, out data))
        {
            return null;
        }

        return data as Dictionary<string, string>;
    }

    public Dictionary<string, string> ToProtectedClaims(string data)
    {
        if (data == null) return new Dictionary<string, string>();
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
    }

    public void Cache(Dictionary<string, string> claims)
    {
        _protectedClaims = claims;
    }
}