using Microsoft.Extensions.DependencyInjection;
using VehicleManagement.User.Repositories;
using VehicleManagement.User.Repositories.Interfaces;
using VehicleManagement.User.Services;
using VehicleManagement.User.Services.Interfaces;
using VehicleManagement.User.Validator;
using VehicleManagement.User.Validator.Interfaces;

namespace VehicleManagement.User.Application;

public static class DiConfiguration
{
    public static void UseUserConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>().
            AddScoped<IUserService, UserService>()
            .AddScoped<IUserValidator, UserValidator>();
    }
}