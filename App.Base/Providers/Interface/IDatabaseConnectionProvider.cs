using Npgsql;

namespace App.Base.Providers.Interface
{
    public interface IDatabaseConnectionProvider
    {
        NpgsqlConnection GetConnection();
        string GetConnection(string databaseName);
    }
}