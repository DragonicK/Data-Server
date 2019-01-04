namespace Data_Server.Data {
    public sealed class Account {
        public string AccountName { get; set; }
        public string Password { get; set; }
        public int AccountID { get; set; }
        public byte Banned { get; set; }
        public int UserGroup { get; set; }
        public byte ServiceID { get; set; }
    }
}