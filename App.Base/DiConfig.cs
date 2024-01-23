using System;
using System.Linq;
using App.Base.DataContext;
using App.Base.DataContext.Interfaces;
using App.Base.Providers;
using App.Base.Providers.Interface;
using App.Base.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace App.Base
{
    public static class DiConfig
    {
        public static IServiceCollection UseBase(this IServiceCollection services)
        {
            services.AddScoped<IConnectionProvider, ConnectionProvider>()
                .AddScoped<IUow, Uow>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            return services;
        }
    }
}