using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Communication;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpSaveCharacterTitle : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var characterId = msg.ReadInt32();
            var count = msg.ReadInt32();

            var title = new List<Title>();

            for (var n = 1; n <= count; n++) {
                title.Add(
                    new Title() {
                        Index = n,
                        ID = msg.ReadInt32()   
                    }
                );
            }

            msg.Flush();
            msg = null;

            AddCharacterTitle(characterId, ref title);
        }

        private void AddCharacterTitle(int characterId, ref List<Title> title) {
            var logs = $"Received Title Character Id: {characterId}";
            var logColor = LogColor.Coral;

            var character = Global.FindCharacterById(characterId);

            if (character != null) {
                character.Title = title;
            }
            else {
                logs = $"Title Character Id: {characterId} Not Found";
                logColor = LogColor.Red;
            }

            Global.WriteLog(LogType.Player, logs, logColor);
        }
    }
}