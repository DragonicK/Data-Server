using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterHotbar : SendPacket {
        public SpCharacterHotbar(int userIndex, List<Hotbar> hotbars) {
            var count = hotbars.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(count);

            for (int i = 0; i < count; i++) {
                msg.Write(hotbars[i].Index);
                msg.Write(hotbars[i].Value);
                msg.Write(hotbars[i].Type);
            }
        }
    }
}