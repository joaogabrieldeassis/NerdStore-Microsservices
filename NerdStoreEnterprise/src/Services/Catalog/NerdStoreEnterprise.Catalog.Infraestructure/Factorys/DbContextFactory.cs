using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NerdStoreEnterprise.Catalog.Infraestructure.Factorys;

public class DbContextFactory(IConfiguration configuration) : IDisposable
{
    private readonly IConfiguration _configuration = configuration;

    public SqlConnection CreateConnection()
    {
        try
        {
            var connection = _configuration.GetConnectionString("DefaultConnectionSql");
            return new SqlConnection(connection);
        }
        catch (Exception e)
        {
            throw new Exception("Não foi possivel estabelecer uma conexão com o banco de dados", e.InnerException);
        }
    }

    public void Dispose()
        => CreateConnection()?.Dispose();
}