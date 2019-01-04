using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterCraft : SendPacket  {
        public SpCharacterCraft(int userIndex, List<Craft> craft) {
            var craftCount = craft.Count;
            
            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(craftCount);

            for (int i = 0; i < craftCount; i++) {
                var recipeCount = craft[i].Recipes.Count;

                msg.Write(craft[i].Index);
                msg.Write(craft[i].Type);
                msg.Write(craft[i].Level);
                msg.Write(craft[i].Experience);
                msg.Write(recipeCount);

                for (int n = 0; n < recipeCount; n++) {
                    msg.Write(craft[i].Recipes[n]);
                }
            }
        }
    }
}