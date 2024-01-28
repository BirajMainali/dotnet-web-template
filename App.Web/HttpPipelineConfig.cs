using App.Base.Providers;
using App.Base.Settings;
using App.Web.Middlewares;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.Extensions.Options;
using Serilog;

namespace App.Web;

public static class HttpPipelineConfig
{
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseCors("AllowAll");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App.Web v1"));
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

        var useMultiTenant = app.Services.GetService<IOptions<AppSettings>>()!.Value.UseMultiTenancy;

        if (useMultiTenant)
        {
            app.UseMultiTenant();
        }

        app.UseNotyf();
        app.MapControllerRoute(
            name: "areaRoute",  pattern: "{area:exists}/{controller=Auth}/{action=Index}/{id?}"
        ).RequireAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();
        return app;
    }
}