using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterSpellBuff : SendPacket {
        public SpCharacterSpellBuff(int userIndex, List<SpellBuff> spellBuffs) {
            var count = spellBuffs.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(count);

            for (int i = 0; i < count; i++) {
                msg.Write(spellBuffs[i].Index);
                msg.Write(spellBuffs[i].ID);
                msg.Write(spellBuffs[i].Level);
                msg.Write(spellBuffs[i].Duration);
            }
        }
    }
}