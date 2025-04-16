using Microsoft.Data.SqlClient;
using MySqlConnector;
using System.Data;

namespace OrderManagementAPI.Data
{
    public class OrderContext
    {
        private readonly string _connectionString;

        public OrderContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}