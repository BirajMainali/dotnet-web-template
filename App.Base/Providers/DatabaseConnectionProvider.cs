using System;
using App.Base.Providers.Interface;
using App.Base.Settings;
using Microsoft.Extensions.Options;
using Npgsql;

namespace App.Base.Providers
{
    public class DatabaseConnectionProvider : IDatabaseConnectionProvider
    {
        private readonly IOptions<AppSettings> _appSettings;

        public DatabaseConnectionProvider(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        /// <summary>
        /// Ready to use NpgsqlConnection
        /// </summary>
        /// <returns></returns>
        public NpgsqlConnection GetConnection()
        {
            var defaultConnection = _appSettings.Value.ConnectionStrings.DefaultConnection;
            return new NpgsqlConnection(defaultConnection);
        }

        public string GetConnectionString(string databaseName)
        {
            var defaultConnection = _appSettings.Value.ConnectionStrings.DefaultConnection;
            var builder = new NpgsqlConnectionStringBuilder(defaultConnection)
            {
                Database = databaseName
            };
            return builder.ConnectionString;
        }
    }
}