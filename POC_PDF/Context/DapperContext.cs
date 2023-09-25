using System.Data;
using System.Data.SqlClient;

namespace POC_PDF.Context;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqlConnection");
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}