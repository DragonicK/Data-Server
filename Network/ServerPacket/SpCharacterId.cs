namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterId : SendPacket {
        public SpCharacterId(int index, int characterId) {
            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(index);
            msg.Write(characterId);
        }
    }
}