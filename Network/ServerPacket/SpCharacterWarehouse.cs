using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterWarehouse : SendPacket {
        public SpCharacterWarehouse(int userIndex, List<Inventory> warehouse) {
            var count = warehouse.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(count);

            for (int i = 0; i < count; i++) {
                msg.Write(warehouse[i].Index);
                msg.Write(warehouse[i].ItemID);
                msg.Write(warehouse[i].ItemValue);
                msg.Write(warehouse[i].ItemLevel);
                msg.Write(warehouse[i].ItemBound);
            }
        }
    }
}