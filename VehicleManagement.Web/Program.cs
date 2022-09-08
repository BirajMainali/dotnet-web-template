using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Web;
using VehicleManagement.Web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.UseConfiguration();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x => { x.LoginPath = "/Auth"; });
var app = builder.Build();
app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()?.Database.Migrate();
app.ConfigurePipeline();
app.UseAuthentication();
app.Run();