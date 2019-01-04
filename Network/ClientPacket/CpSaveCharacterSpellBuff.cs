using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Communication;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpSaveCharacterSpellBuff : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var characterId = msg.ReadInt32();
            var count = msg.ReadInt32();

            var spellBuff = new List<SpellBuff>();

            for(var n = 1; n <= count; n++) {
                spellBuff.Add(
                    new SpellBuff() {
                        Index = n,
                        ID = msg.ReadInt32(),
                        Level = msg.ReadInt32(),
                        Duration = msg.ReadInt32()
                    }
                );
            }

            msg.Clear();
            msg = null;

            AddCharacterSpellBuff(characterId, ref spellBuff);
        }

        private void AddCharacterSpellBuff(int characterId, ref List<SpellBuff> spellBuff) {
            var logs = $"Received Spell Buff Character Id: {characterId}";
            var logColor = LogColor.Coral;

            var character = Global.FindCharacterById(characterId);

            if (character != null) {
                character.SpellBuff = spellBuff;
            }
            else {
                logs = $"Spell Buff Character Id: {characterId} Not Found";
                logColor = LogColor.Red;
            }

            Global.WriteLog(LogType.Player, logs, logColor);
        }
    }
}