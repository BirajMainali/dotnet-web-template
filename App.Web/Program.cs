using App.Web;
using App.Web.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.UseApp();

var app = builder.Build();


app.UseSerilogRequestLogging();

app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()?.Database.Migrate();

app.ConfigurePipeline().Run();