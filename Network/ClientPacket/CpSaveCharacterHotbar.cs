using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Communication;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpSaveCharacterHotbar : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var characterId = msg.ReadInt32();
            var count = msg.ReadInt32();

            var hotbar = new List<Hotbar>();

            for (int n = 1; n <= count; n++) {
                hotbar.Add(
                    new Hotbar() {
                    Index = n,
                    Value = msg.ReadInt32(),
                    Type = msg.ReadByte()
                    }
                );
            }

            msg.Flush();
            msg = null;

            AddCharacterHotbar(characterId, ref hotbar);
        }

        private void AddCharacterHotbar(int characterId, ref List<Hotbar> hotbar) {
            var logs = $"Received Hotbar Character Id: {characterId}";
            var logColor = LogColor.Coral;

            var character = Global.FindCharacterById(characterId);

            if (character != null) {
                character.Hotbar = hotbar;
            }
            else {
                logs = $"Hotbar Character Id: {characterId} Not Found";
                logColor = LogColor.Red;
            }

            Global.WriteLog(LogType.Player, logs, logColor);
        }
    }
}