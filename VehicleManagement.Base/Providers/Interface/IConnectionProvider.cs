using Npgsql;

namespace Base.Providers.Interface
{
    public interface IConnectionProvider
    {
        NpgsqlConnection GetConnection();
    }
}