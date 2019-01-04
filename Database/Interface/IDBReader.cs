namespace Data_Server.Database.Interface {
    public interface IDBDataReader {
        void Close();
        bool Read();
        object GetData(string column);
    }
}