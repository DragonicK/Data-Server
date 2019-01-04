using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Communication;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpSaveCharacterVariable : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var characterId = msg.ReadInt32();
            var count = msg.ReadInt32();

            var variable = new List<Variable>();

            for(var n = 1; n <= count; n++) {
                variable.Add(
                    new Variable {
                        ID = n,
                        Value = msg.ReadInt32()
                    }
                );
            }

            msg.Clear();
            msg = null;

            AddCharacterVariable(characterId, ref variable);
        }

        private void AddCharacterVariable(int characterId, ref List<Variable> variable) {
            var logs = $"Received Variable Character Id: {characterId}";
            var logColor = LogColor.Coral;

            var character = Global.FindCharacterById(characterId);

            if (character != null) {
                character.Variable = variable;
            }
            else {
                logs = $"Variable Character Id: {characterId} Not Found";
                logColor = LogColor.Red;
            }

            Global.WriteLog(LogType.Player, logs, logColor);
        }
    }
}