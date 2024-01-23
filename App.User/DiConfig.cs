using App.User.Services;
using App.User.Services.Interfaces;
using App.User.Validator;
using App.User.Validator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace App.User;

public static class DiConfiguration
{
    public static IServiceCollection UseUserConfiguration(this IServiceCollection services)
        => services.AddScoped<IUserService, UserService>()
            .AddScoped<IUserValidator, UserValidator>();
}