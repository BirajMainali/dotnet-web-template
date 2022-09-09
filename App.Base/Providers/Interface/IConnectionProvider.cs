using Npgsql;

namespace App.Base.Providers.Interface
{
    public interface IConnectionProvider
    {
        NpgsqlConnection GetConnection();
    }
}