using Microsoft.Extensions.DependencyInjection;
using NepaliDateConverter.Interfaces;

namespace NepaliDateConverter
{
    public static class DiConfig
    {
        public static IServiceCollection UseNepaliDate(this IServiceCollection services)
            => services.AddSingleton<INepaliDateConverter, NepaliDateConverter>();
    }
}