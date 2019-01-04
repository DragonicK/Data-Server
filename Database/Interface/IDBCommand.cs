namespace Data_Server.Database.Interface {
    public interface IDBCommand {
        void AddParameter(string parameter, object value);
        int Execute();
        IDBDataReader ExecuteReader();
        void SetCommand(string commandText);
        void SetCommandType(DBCommandType dBCommandType);
        void ClearParameter();
    }
}