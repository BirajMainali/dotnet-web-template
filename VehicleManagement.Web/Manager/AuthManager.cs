using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using VehicleManagement.User.Crypter;
using VehicleManagement.User.Repositories.Interfaces;
using VehicleManagement.Web.Manager.Interfaces;
using VehicleManagement.Web.ValueObject;

namespace VehicleManagement.Web.Manager;

public class AuthManager : IAuthManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public AuthManager(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public async Task<AuthResult> Login(string identity, string password)
    {
        var user = await _userRepository.GetItemAsync(x => x.Email.ToLower() == identity.ToLower().Trim());
        var result = new AuthResult();
        if (user == null)
        {
            result.Success = false;
            result.Errors.Add("User not found");
            return result;
        }

        if (!Crypter.Verify(password, user.Password))
        {
            result.Success = false;
            result.Errors.Add("Invalid password");
            return result;
        }

        var httpContext = _httpContextAccessor.HttpContext;
        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString())
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        result.Success = true;
        return result;
    }
}