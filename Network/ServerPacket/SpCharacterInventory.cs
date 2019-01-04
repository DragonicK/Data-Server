using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterInventory : SendPacket {
        public SpCharacterInventory(int userIndex, List<Inventory> inventory) {
            var count = inventory.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(count);

            for (int i = 0; i < count; i++) {
                msg.Write(inventory[i].Index);
                msg.Write(inventory[i].ItemID );
                msg.Write(inventory[i].ItemValue);
                msg.Write(inventory[i].ItemLevel);
                msg.Write(inventory[i].ItemBound);
            }
        }
    }
}