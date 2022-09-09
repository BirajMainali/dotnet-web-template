using App.Base.DataContext;
using App.Base.DataContext.Interfaces;
using App.Base.Providers;
using App.Base.Providers.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace App.Base
{
    public static class DiConfig
    {
        public static IServiceCollection UseBase(this IServiceCollection service)
            => service.AddScoped<IConnectionProvider, ConnectionProvider>()
                .AddScoped<CustomFileProvider>()
                .AddScoped<IUow, Uow>();
    }
}