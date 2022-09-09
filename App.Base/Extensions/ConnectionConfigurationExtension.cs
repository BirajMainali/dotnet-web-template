using Microsoft.Extensions.Configuration;

namespace App.Base.Extensions
{
    public static class ConnectionConfigurationExtension
    {
        /// <summary>
        /// Use Default Connection string
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string GetDefaultConnectionString(this IConfiguration configuration)
            => configuration.GetConnectionString("DefaultConnection");
    }
}