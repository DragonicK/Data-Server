namespace Data_Server.Network {
    public abstract class SendPacket {
        protected ByteBuffer msg;

        public SendPacket() {
            msg = new ByteBuffer();
        }

        public void Send(IConnection connection) {
            ((Connection)connection).Send(msg, GetType().Name);

            msg.Flush();
            msg = null;
        }
    }
}