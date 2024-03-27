using DapperSPTest.Abstract;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperSPTest.Concrete
{
    public class DbContext : IDbContext
    {
        public IDbConnection CreateConnection()
        {
            var connStr = Environment.GetEnvironmentVariable("DB_CONNECTION");
            return new SqlConnection(connStr);
        }
    }
}
