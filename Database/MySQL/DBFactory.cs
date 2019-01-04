using Data_Server.Database.Interface;

namespace Data_Server.Database.MySQL {
    public sealed class DBFactory : IDBFactory {
        public IDBCommand GetCommand(IDBConnection dBConnection) {
            return new DBCommand(dBConnection);
        }

        public IDBConnection GetConnection() {
            return new DBConnection();
        }
    }
}