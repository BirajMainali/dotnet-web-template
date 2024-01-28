using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.Base.Constants;
using App.Base.Repository;
using App.Base.Settings;
using App.User.Crypter;
using App.User.Entity;
using App.User.Handler;
using App.User.Manager;
using App.Web.Manager.Interfaces;
using App.Web.ValueObject;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App.Web.Manager;

public class Authenticator : IAuthenticator
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<AppUser, long> _userRepository;
    private readonly IOptions<AppSettings> _options;
    private readonly IMultiTenantClaimManager _multiTenantClaimManager;

    public Authenticator(IHttpContextAccessor httpContextAccessor, IRepository<AppUser, long> userRepository, IOptions<AppSettings> options, IMultiTenantClaimManager multiTenantClaimManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _options = options;
        _multiTenantClaimManager = multiTenantClaimManager;
    }

    public async Task<AuthResult> AuthenticateThoughToken(string email, string password)
    {
        var user = await _userRepository.GetItemAsync(x => x.Email.ToLower() == email.ToLower().Trim());
        var result = new AuthResult();
        if (user != null && Crypter.Verify(password, user.Password))
        {
            var connectionKey = user.ParentUserId.HasValue ? user.ParentUser!.Email : user.Email;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.JwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(AuthenticationKeyConstants.AuthenticationKey, user.Id.ToString()),
                    new Claim(AuthenticationKeyConstants.MultiTenantAuthenticationKey, _multiTenantClaimManager.GetProtectedClaim(connectionKey))
                }, JwtBearerDefaults.AuthenticationScheme),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            result.Token = tokenHandler.WriteToken(token);
            var httpContext = _httpContextAccessor.HttpContext;
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(tokenDescriptor.Subject));
            result.Success = true;
        }
        else
        {
            result.Success = false;
            result.Errors.Add("Invalid credentials");
        }

        return result;
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

        var connectionKey = user.ParentUserId.HasValue ? user.ParentUser!.Email : user.Email;

        var httpContext = _httpContextAccessor.HttpContext;
        var claims = new List<Claim>
        {
            new(AuthenticationKeyConstants.AuthenticationKey, user.Id.ToString()),
            new(AuthenticationKeyConstants.MultiTenantAuthenticationKey, _multiTenantClaimManager.GetProtectedClaim(connectionKey))
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
        result.Success = true;
        return result;
    }
}