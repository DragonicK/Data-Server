using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterCurrency : SendPacket {
        public SpCharacterCurrency(int userIndex, List<Currency> currency) {
            var count = currency.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(count);

            for (int i = 0; i < count; i++) {
                msg.Write(currency[i].Value);
            }
        }
    }
}