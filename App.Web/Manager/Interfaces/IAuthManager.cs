using App.Web.ValueObject;

namespace App.Web.Manager.Interfaces;

public interface IAuthManager
{
    Task<AuthResult> Login(string identity, string password);
}