using App.Base.Configurations;

namespace App.User.Provider;

public interface IProtectedClaimProvider : ITransientDependency
{
    Dictionary<string, string> GetProtectedClaims();
    Dictionary<string, string> ToProtectedClaims(string data);
    void Cache(Dictionary<string, string> claims);
}