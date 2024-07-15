using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class DbConnection
{
    public SqlConnection GetConnection()
    {
        var conn = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("ReviewsDb");
        return new SqlConnection(conn);
    }
}