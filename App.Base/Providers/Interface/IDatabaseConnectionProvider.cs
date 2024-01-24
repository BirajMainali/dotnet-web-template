using App.Base.Configurations;
using Npgsql;

namespace App.Base.Providers.Interface
{
    public interface IDatabaseConnectionProvider : IScopedDependency
    {
        NpgsqlConnection GetConnection();
        string GetConnectionString(string databaseName);
    }
}