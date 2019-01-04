using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterEquipment : SendPacket {
        public SpCharacterEquipment(int userIndex, List<Inventory> equipments) {
            var count = equipments.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(count);

            for (int i = 0; i < count; i++) {
                msg.Write(equipments[i].Index);
                msg.Write(equipments[i].ItemID);
                msg.Write(equipments[i].ItemLevel);
                msg.Write(equipments[i].ItemBound);
            }
        }
    }
}