using System.Reflection;
using App.Base.Configurations;

namespace App.Web
{
    public static class ConfigureServicesFromAssembly
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
            toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));

            foreach (var assembly in loadedAssemblies)
            {
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