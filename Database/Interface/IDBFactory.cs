namespace Data_Server.Database.Interface {
    public interface IDBFactory {
        IDBCommand GetCommand(IDBConnection dBConnection);
        IDBConnection GetConnection();
    }
}