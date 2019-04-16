using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Communication;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpSaveCharacterAchievement : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var characterId = msg.ReadInt32();
            var achievementCount = msg.ReadInt32();

            var achievement = new List<Achievement>();

            for (var n = 1; n <= achievementCount; n++) {
                   var value = new Achievement() {
                       Index = n,
                       Value = msg.ReadByte()
                   };

                achievement.Add(value);
            }

            msg.Flush();
            msg = null;

            AddCharacterAchievement(characterId, ref achievement);
        }

        private void AddCharacterAchievement(int characterId, ref List<Achievement> achievement) {
            var logs = $"Received Achievement Character Id: {characterId}";
            var logColor = LogColor.Coral;

            var character = Global.FindCharacterById(characterId);

            if (character != null) {
                character.Achievement = achievement;
            }
            else {
                logs = $"Achievement Character Id: {characterId} Not Found";
                logColor = LogColor.Red;
            }

            Global.WriteLog(LogType.Player, logs, logColor);
        }
    }
}