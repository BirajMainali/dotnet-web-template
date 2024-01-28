using App.Base.Constants;
using App.Base.Providers.Interface;
using App.User.Manager;
using App.User.Provider;
using App.Web.Providers.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Middlewares;

public class MultiTenantResolverMiddleware
{
    private readonly RequestDelegate _next;

    public MultiTenantResolverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IMultiTenantClaimManager multiTenantClaimManager,
        ICurrentUserProvider currentUserProvider, DbContext dbContext, IProtectedClaimProvider protectedClaimProvider,
        IDatabaseConnectionProvider databaseConnectionProvider)
    {
        try
        {
            var connectionKey = multiTenantClaimManager.GetMultiTenantConnectionKey();
            var isAuthorized = context.User.Identity.IsAuthenticated || currentUserProvider.IsLoggedIn();
            if (!isAuthorized)
            {
                multiTenantClaimManager.RemoveClaims();
            }

            if (isAuthorized && connectionKey == null)
            {
                throw new Exception("Invalid token");
            }
            else if (isAuthorized)
            {
                var connectionString = databaseConnectionProvider.GetConnectionString(connectionKey);
                dbContext.Database.SetConnectionString(connectionString);
            }
        }
        catch (Exception e)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            multiTenantClaimManager.RemoveClaims();
            context.Response.Redirect("/");
            return;
        }

        await _next.Invoke(context);
    }
}

public static class MultiTenantClaimMiddlewareExtension
{
    public static IApplicationBuilder UseMultiTenant(this IApplicationBuilder app)
        => app.UseMiddleware<MultiTenantResolverMiddleware>();
}