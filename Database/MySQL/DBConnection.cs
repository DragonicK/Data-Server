using System.Data;
using MySql.Data.MySqlClient;
using Data_Server.Communication;
using Data_Server.Database.Interface;

namespace Data_Server.Database.MySQL {
    public sealed class DBConnection : IDBConnection {
        public MySqlConnection Connection { get; set; }

        private readonly string connectionString;

        public DBConnection() {
            connectionString = $"Server={Configuration.Server};";
            connectionString += $"Database={Configuration.Database};";
            connectionString += $"Uid={Configuration.Username};";
            connectionString += $"Pwd={Configuration.Password};";
            connectionString += $"MinimumPoolSize={Configuration.MinPoolSize};";
            connectionString += $"MaximumPoolSize={Configuration.MaxPoolSize};";
            connectionString += "Pooling=true;";
            connectionString += "SSL Mode = None;";

            Connection = new MySqlConnection();
        }
        
        public DBError Open() {
            var dbError = new DBError();

            Connection.ConnectionString = connectionString;

            try {
                Connection.Open();
            }
            catch (MySqlException ex) {
                dbError.Number = ex.Message.Length;
                dbError.Message = ex.Message;
            }

            return dbError;
        }

        public void Close() {
            Connection.Close();
            Connection.Dispose();
        }

        public bool IsOpen() {
            return (Connection.State == ConnectionState.Open) ? true : false;
        }
    }
}