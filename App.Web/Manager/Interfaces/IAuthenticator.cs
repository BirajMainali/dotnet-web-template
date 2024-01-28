using App.Web.ValueObject;

namespace App.Web.Manager.Interfaces;

public interface IAuthenticator
{
    Task<AuthResult> Login(string identity, string password);
    Task<AuthResult> AuthenticateThoughToken(string email, string password);
}