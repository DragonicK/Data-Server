using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterAchievement : SendPacket {
        public SpCharacterAchievement(int userIndex, List<Achievement> achievement) {
            var length = achievement.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(length);

            for (int i = 0; i < length; i++) {
                msg.Write(achievement[i].Index);
                msg.Write(achievement[i].Value);
            }
        }
    }
}