namespace Data_Server.Database.Interface {
    public interface IDBConnection {
        DBError Open();
        void Close();
        bool IsOpen();
    }
}