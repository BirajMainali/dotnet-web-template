using App.Base.Extensions;
using App.Base.Providers.Interface;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace App.Base.Providers
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly IConfiguration _configuration;

        public ConnectionProvider(IConfiguration configuration)
            => _configuration = configuration;

        /// <summary>
        /// Ready to use NpgsqlConnection
        /// </summary>
        /// <returns></returns>
        public NpgsqlConnection GetConnection()
        {
            var connectionString = _configuration.GetDefaultConnectionString();
            return new NpgsqlConnection(connectionString);
        }
    }
}