using System;
using System.Linq;
using System.Reflection;
using App.Base.Configurations;
using App.Base.DataContext;
using App.Base.DataContext.Interfaces;
using App.Base.Providers;
using App.Base.Providers.Interface;
using App.Base.Repository;
using App.Base.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace App.Base
{
    public static class DiConfig
    {
        public static IServiceCollection UseBase(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseConnectionProvider, DatabaseConnectionProvider>().AddScoped<IUow, Uow>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            return services;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var assemblyString = Assembly.GetExecutingAssembly().GetName().Name;
            if (assemblyString != null)
            {
                var assembly = AppDomain.CurrentDomain.Load(assemblyString);
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(ITransientDependency).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        var interfaceType = type.GetInterfaces().FirstOrDefault(i => typeof(ITransientDependency).IsAssignableFrom(i));
                        if (interfaceType != null)
                        {
                            services.AddTransient(interfaceType, type);
                        }
                    }

                    if (typeof(IScopedDependency).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        var interfaceType = type.GetInterfaces().FirstOrDefault(i => typeof(IScopedDependency).IsAssignableFrom(i));
                        if (interfaceType != null)
                        {
                            services.AddScoped(interfaceType, type);
                        }
                    }
                }
            }
        }
    }
}