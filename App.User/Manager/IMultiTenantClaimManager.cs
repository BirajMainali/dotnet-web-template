using App.Base.Configurations;

namespace App.User.Manager;

public interface IMultiTenantClaimManager : IScopedDependency
{
    string SaveClaims(Dictionary<string, string> claims);
    string GetClaimsFromRequest();
    void RemoveClaims();
}