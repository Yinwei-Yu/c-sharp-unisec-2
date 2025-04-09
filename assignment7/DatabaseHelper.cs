using MySql.Data.MySqlClient;
using System.Configuration;

namespace Week6;

public static class DatabaseHelper
{
    private const string Server = "localhost";
    private const string Database = "c#_orders";
    private const string UserId = "root";
    private const string Password = "23333";

    public static MySqlConnection CreateNewConnection()
    {
        var builder = new MySqlConnectionStringBuilder
        {
            Server = Server,
            Database = Database,
            UserID = UserId,
            Password = Password,
            Pooling = true
        };
        return new MySqlConnection(builder.ConnectionString);
    }

    public static void ExecuteNonQuery(string sql, Action<MySqlParameterCollection> configureParameters = null)
    {
        using var connection = CreateNewConnection();
        using var cmd = new MySqlCommand(sql, connection);
        
        try
        {
            connection.Open();
            configureParameters?.Invoke(cmd.Parameters);
            cmd.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            System.Windows.Forms.MessageBox.Show($"数据库操作失败: {ex.Message}");
            throw;
        }
    }
    
    public static T ExecuteScalar<T>(string sql, Action<MySqlParameterCollection> configureParameters = null)
    {
        using var connection = CreateNewConnection();
        using var cmd = new MySqlCommand(sql, connection);
        
        try
        {
            connection.Open();
            configureParameters?.Invoke(cmd.Parameters);
            return (T)Convert.ChangeType(cmd.ExecuteScalar(), typeof(T));
        }
        catch (MySqlException ex)
        {
            System.Windows.Forms.MessageBox.Show($"数据库查询失败: {ex.Message}");
            throw;
        }
    }
    
    public static void ExecuteReader(string sql, Action<MySqlDataReader> handleReader, Action<MySqlParameterCollection> configureParameters = null)
    {
        using var connection = CreateNewConnection();
        using var cmd = new MySqlCommand(sql, connection);
        
        try
        {
            connection.Open();
            configureParameters?.Invoke(cmd.Parameters);
            using var reader = cmd.ExecuteReader();
            handleReader(reader);
        }
        catch (MySqlException ex)
        {
            System.Windows.Forms.MessageBox.Show($"数据库查询失败: {ex.Message}");
            throw;
        }
    }
}