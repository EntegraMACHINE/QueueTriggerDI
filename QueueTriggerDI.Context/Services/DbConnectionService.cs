using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QueueTriggerDI.Context.Services
{
    public class DbConnectionService : IDbConnectionService
    {
        private readonly string connectionString;

        public DbConnectionService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("LocalDatabaseConnectionString");
        }

        public IDbConnection CreateConnection()
        {
            IDbConnection connection = new SqlConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}
