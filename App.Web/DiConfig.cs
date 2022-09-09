using App.Base;
using App.User;
using App.Web.Data;
using App.Web.Manager;
using App.Web.Manager.Interfaces;
using App.Web.Providers;
using App.Web.Providers.Interfaces;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace App.Web;

public static class ApplicationDiConfig
{
    public static void UseApp(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddNotyf(config =>
        {
            config.DurationInSeconds = 10;
            config.IsDismissable = true;
            config.Position = NotyfPosition.BottomRight;
        });

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(x => { x.LoginPath = "/Auth"; });

        builder.Services.UseUserConfiguration()
            .UseBase();

        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>()
            .AddScoped<DbContext, ApplicationDbContext>()
            .AddScoped<IAuthManager, AuthManager>().AddHttpContextAccessor();
    }
}