using Base.DataContext;
using Base.DataContext.Interfaces;
using Base.Providers;
using Base.Providers.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Application
{
    public static class ApplicationDiConfig
    {
        public static IServiceCollection UseBase(this IServiceCollection service)
            => service.AddScoped<IConnectionProvider, ConnectionProvider>()
            .AddScoped<IUow,Uow>();
    }
}