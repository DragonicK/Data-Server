using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterSpell : SendPacket {
        public SpCharacterSpell(int userIndex, List<Spell> spells) {
            var count = spells.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(count);

            for(int i = 0; i < count; i++) {
                msg.Write(spells[i].Index);
                msg.Write(spells[i].ID);
                msg.Write(spells[i].Level);
                msg.Write(spells[i].Uses);
            }
        }
    }
}