using App.Base.Providers;
using AspNetCoreHero.ToastNotification.Extensions;

namespace App.Web;

public static class HttpPipelineConfig
{
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new CustomFileProvider(Path.Combine(app.Environment.ContentRootPath, "Content")),
            RequestPath = "/Content"
        });
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseNotyf();
        app.MapControllerRoute(
            name: "areaRoute",
            pattern: "{area:exists}/{controller=Auth}/{action=Index}/{id?}"
        ).RequireAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();
        return app;
    }
}