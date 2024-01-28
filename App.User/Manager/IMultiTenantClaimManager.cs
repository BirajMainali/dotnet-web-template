using App.Base.Configurations;

namespace App.User.Manager;

public interface IMultiTenantClaimManager : IScopedDependency
{
    string GetProtectedClaim(string key);
    string GetMultiTenantConnectionKey();
    void RemoveClaims();
}