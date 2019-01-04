using System;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpExistName : SendPacket {
        public SpExistName(int userIndex, bool result, string name, byte sex, byte classe, int sprite, int charnum) {
            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(Convert.ToByte(result));
            msg.Write(name);
            msg.Write(sex);
            msg.Write(classe);
            msg.Write(sprite);
            msg.Write(charnum);
        }
    }
}