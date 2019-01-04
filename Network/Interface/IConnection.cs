namespace Data_Server.Network {
    public interface IConnection {
        void Send(ByteBuffer msg, string className);
    }
}