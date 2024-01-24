using App.Web;
using App.Web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.UseApp();

var app = builder.Build();
app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()?.Database.Migrate();
app.ConfigurePipeline().Run();