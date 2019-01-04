using Data_Server.Data;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpCharacterData : SendPacket {
        public SpCharacterData(int userIndex, Character character) {
            msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(userIndex);
            msg.Write(character.CharacterId);
            msg.Write(character.CharacterIndex);
            msg.Write(character.Access);
            msg.Write(character.Name);
            msg.Write(character.Classe);
            msg.Write(character.Sprite);
            msg.Write(character.OriginalSprite);
            msg.Write(character.Level);
            msg.Write(character.Experience);
            msg.Write(character.HP);
            msg.Write(character.MP);
            msg.Write(character.Strength);
            msg.Write(character.Endurance);
            msg.Write(character.Intelligence);
            msg.Write(character.Agility);
            msg.Write(character.Vitality);
            msg.Write(character.Dexterity);
            msg.Write(character.Points);
            msg.Write(character.PK);
            msg.Write(character.Map);
            msg.Write(character.X);
            msg.Write(character.Y);
            msg.Write(character.Dir);
            msg.Write(character.TitleID );
            msg.Write(character.TitleVisibility);
            msg.Write(character.Muted);
            msg.Write(character.TutorialState);
            msg.Write(character.CostumeId);
            msg.Write(character.BindMap);
            msg.Write(character.BindX);
            msg.Write(character.BindY);       
        }
    }
}