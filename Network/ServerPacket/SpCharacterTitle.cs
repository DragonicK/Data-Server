using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterTitle : SendPacket {
        public SpCharacterTitle(int userIndex, List<Title> titles) {
            var count = titles.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(count);

            for (int i = 0; i < count; i++) {
                msg.Write(titles[i].Index);
                msg.Write(titles[i].ID);
            }
        }
    }
}