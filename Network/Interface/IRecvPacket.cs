namespace Data_Server.Network {
    public interface IRecvPacket {
        void Process(byte[] buffer, IConnection connection);
    }
}