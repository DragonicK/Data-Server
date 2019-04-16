using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Communication;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpSaveCharacterSpell : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var characterId = msg.ReadInt32();
            var count = msg.ReadInt32();

            var spell = new List<Spell>();

            for(var n = 1; n <= count; n++) {
                spell.Add(
                    new Spell() {
                        Index = n,
                        ID = msg.ReadInt32(),
                        Uses = msg.ReadInt32(),
                        Level = msg.ReadInt32()
                    }
                );
            }

            msg.Flush();
            msg = null;

            AddCharacterSpell(characterId, ref spell);
        }

        private void AddCharacterSpell(int characterId, ref List<Spell> spell) {
            var logs = $"Received Spell Character Id: {characterId}";
            var logColor = LogColor.Coral;

            var character = Global.FindCharacterById(characterId);

            if (character != null) {
                character.Spell = spell;
            }
            else {
                logs = $"Spell Character Id: {characterId} Not Found";
                logColor = LogColor.Red;
            }

            Global.WriteLog(LogType.Player, logs, logColor);
        }
    }
}