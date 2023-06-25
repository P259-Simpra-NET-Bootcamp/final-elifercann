using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Data.SqlClient;

namespace simApi.Data.Context;

public class DapperSimDbContext
{
    private readonly IConfiguration configuration;
    private readonly string connectionString;
    private readonly string databaseType;

    public DapperSimDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
        this.databaseType = configuration.GetConnectionString("DbType");
        this.connectionString = GetConnection();
    }


    private string GetConnection()
    {
        switch (this.databaseType)
        {
            case "Mssql":
                return configuration.GetConnectionString("MsSqlConnection");
            case "PostgreSql":
                return configuration.GetConnectionString("PostgreSqlConnection");
            default:
                return configuration.GetConnectionString("DefaultConnection");
        }
    }

    public IDbConnection CreateConnection()
    {
        switch (this.databaseType)
        {
            case "Mssql":
                return new SqlConnection(connectionString);
            case "PostgreSql":
                return new NpgsqlConnection(connectionString);
            default:
                return new SqlConnection(connectionString);
        }
    }
}