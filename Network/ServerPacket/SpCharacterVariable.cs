using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterVariable : SendPacket {
        public SpCharacterVariable(int userIndex, List<Variable> variables) {
            var count = variables.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(count);

            for (int i = 0; i < count; i++) {
                msg.Write(variables[i].ID);
                msg.Write(variables[i].Value);
            }
        }
    }
}