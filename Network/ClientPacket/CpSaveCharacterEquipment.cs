using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Communication;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpSaveCharacterEquipment : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var characterId = msg.ReadInt32();
            var count = msg.ReadInt32();

            var equipment = new List<Inventory>();

            for (var n = 1; n <= count; n++) {
                equipment.Add(
                    new Inventory {
                        Index = n,
                        ItemID = msg.ReadInt32(),
                     // ItemValue = msg.ReadInt32(),
                        ItemLevel = msg.ReadInt32(),
                        ItemBound = msg.ReadByte()
                    }
                );
            };

            msg.Clear();
            msg = null;

            AddCharacterEquipment(characterId, ref equipment);
        }

        private void AddCharacterEquipment(int characterId, ref List<Inventory> equipment) {
            var logs = $"Received Equipment Character Id: {characterId}";
            var logColor = LogColor.Coral;

            var character = Global.FindCharacterById(characterId);

            if (character != null) {
                character.Equipment = equipment;
            }
            else {
                logs = $"Equipment Character Id: {characterId} Not Found";
                logColor = LogColor.Red;
            }

            Global.WriteLog(LogType.Player, logs, logColor);
        }
    }
}