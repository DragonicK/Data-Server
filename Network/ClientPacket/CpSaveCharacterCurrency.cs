using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Communication;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpSaveCharacterCurrency : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var characterId = msg.ReadInt32();
            var count = msg.ReadInt32();

            var currency = new List<Currency>();

            for (int n = 0; n <= count; n++) {
                currency.Add(
                    new Currency() {
                        Index = n,
                        Value = msg.ReadInt32()
                    }
                );         
            }

            msg.Flush();
            msg = null;

            AddCharacterCurrency(characterId, ref currency);
        }

        private void AddCharacterCurrency(int characterId, ref List<Currency> currency) {
            var logs = $"Received Currency Character Id: {characterId}";
            var logColor = LogColor.Coral;

            var character = Global.FindCharacterById(characterId);

            if (character != null) {
                character.Currency = currency;
            }
            else {
                logs = $"Currency Character Id: {characterId} Not Found";
                logColor = LogColor.Red;
            }

            Global.WriteLog(LogType.Player, logs, logColor);
        }
    }
}