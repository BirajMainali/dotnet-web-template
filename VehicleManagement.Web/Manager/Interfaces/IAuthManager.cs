using VehicleManagement.Web.ValueObject;

namespace VehicleManagement.Web.Manager.Interfaces;

public interface IAuthManager
{
    Task<AuthResult> Login(string identity, string password);
}