using System.Collections.Generic;
using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterList : SendPacket {
        public SpCharacterList(int userIndex, List<TempCharacter> characters) {
            var count = characters.Count;

            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(count);

            for (int i = 0; i < count; i++) {
                msg.Write(characters[i].Index);
                msg.Write(characters[i].CharacterID);
                msg.Write(characters[i].Name);
                msg.Write(characters[i].Sprite);
                msg.Write(characters[i].Classe);
                msg.Write(characters[i].Access);
            }
        }
    }
}