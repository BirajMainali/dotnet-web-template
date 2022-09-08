using AspNetCoreHero.ToastNotification;
using Base.Application;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.User.Application;
using VehicleManagement.Web.Data;
using VehicleManagement.Web.Manager;
using VehicleManagement.Web.Manager.Interfaces;
using VehicleManagement.Web.Providers;
using VehicleManagement.Web.Providers.Interfaces;

namespace VehicleManagement.Web;

public static class ApplicationDiConfig
{
    public static void UseConfiguration(this WebApplicationBuilder builder)
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
        builder.Services.UseUserConfiguration();
        builder.Services.UseBase();
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>()
            .AddScoped<DbContext, ApplicationDbContext>()
            .AddScoped<IAuthManager, AuthManager>().AddHttpContextAccessor();
    }
    

}