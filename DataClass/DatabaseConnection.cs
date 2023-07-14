using System;
using MySql.Data.MySqlClient;

namespace studentInj.DataClass
{
    public interface IDatabaseConnection
    {
        public MySqlConnection GetConnection();
        public string ConnectionString { get; set; }
    }
    public class DatabaseConnection : IDatabaseConnection
    {
        private string? _connectionString; public string ConnectionString
        {
            get
            {
                return _connectionString ?? string.Empty;
            }
            set
            {
                _connectionString = value;
            }
        }
        public DatabaseConnection(string? connectionString)
        {
            if (connectionString != null)
            {
                _connectionString = connectionString;
            }
        }
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
