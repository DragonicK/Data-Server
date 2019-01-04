using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Communication;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpSaveCharacterCraft : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var characterId = msg.ReadInt32();
            var craftCount = msg.ReadInt32();
            var recipeCount = msg.ReadInt32();

            var craft = new List<Craft>();
            
            for (int n = 1; n <= craftCount; n++) {
                craft.Add(
                    new Craft() {
                        Index = (byte)n,
                        Type = msg.ReadByte(),
                        Level = msg.ReadInt32(),
                        Experience = msg.ReadInt32(),
                        Recipes = new List<int>()
                    }
                );

                for (int i = 0; i < recipeCount; i++) {
                    craft[n-1].Recipes.Add(msg.ReadInt32());
                }
            };

            msg.Clear();
            msg = null;

            AddCharacterCraft(characterId, ref craft);
        }

        private void AddCharacterCraft(int characterId, ref List<Craft> craft) {
            var logs = $"Received Craft Character Id: {characterId}";
            var logColor = LogColor.Coral;

            var character = Global.FindCharacterById(characterId);

            if (character != null) {
                character.Craft = craft;
            }
            else {
                logs = $"Craft Character Id: {characterId} Not Found";
                logColor = LogColor.Red;
            }

            Global.WriteLog(LogType.Player, logs, logColor);
        }
    }
}