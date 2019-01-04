namespace Data_Server.Database.Interface {
    public sealed class DBError {
        public int Number { get; set; }
        public string Message { get; set; }

        public DBError() {
            Message = string.Empty;
        }
    }
}