using System.Text;
using App.Base.Constants;
using App.Base.Repository;
using App.Base.Settings;
using App.Web.Data;
using App.Web.Manager;
using App.Web.Manager.Interfaces;
using App.Web.Providers;
using App.Web.Providers.Interfaces;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace App.Web;

public static class ApplicationDiConfig
{
    public static void UseApp(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var secret = jwtSettings.GetSection("Secret").Value;

        
        builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = "smart";
                opt.DefaultChallengeScheme = "smart";
            })
            .AddPolicyScheme("smart", "JWT or Identity Cookie", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                    if (authHeader?.ToLower().StartsWith("bearer ") == true)
                    {
                        return JwtBearerDefaults.AuthenticationScheme;
                    }

                    return CookieAuthenticationDefaults.AuthenticationScheme;
                };
            })
            .AddCookie(cfg =>
            {
                cfg.SlidingExpiration = true;
                cfg.LoginPath = "/Auth/Index";
                cfg.ExpireTimeSpan = TimeSpan.FromDays(2);
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer {token}\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        builder.Services.AddNotyf(config =>
        {
            config.DurationInSeconds = 10;
            config.IsDismissable = true;
            config.Position = NotyfPosition.BottomRight;
        });

        builder.Services.Configure<AppSettings>(builder.Configuration);


        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>()
            .AddScoped<DbContext, ApplicationDbContext>()
            .AddScoped<IAuthenticator, Authenticator>().AddHttpContextAccessor();

        builder.Services.ConfigureServices();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
    }
}