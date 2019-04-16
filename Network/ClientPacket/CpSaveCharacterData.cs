using Data_Server.Data;
using Data_Server.Database;
using Data_Server.Network.ServerPacket;
using Data_Server.Communication;
using Data_Server.Util;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpSaveCharacterData : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var add = false;
            var index = msg.ReadInt32();

            var accountId = msg.ReadInt32();
            var characterId = msg.ReadInt32();

            var character = Global.FindCharacterById(characterId);
            
            if (character == null) {
                add = true;
                character = new Character();
            }

            character.AccountId = accountId;
            character.CharacterId = characterId;

            ReadCharacterData(ref character, ref msg);

            msg.Flush();
            msg = null;

            InsertCharacterData(index, ref character, connection);

            AddCharacterData(ref character, add);
        }

        private void AddCharacterData(ref Character character, bool add) {
            var logs = $"Received Character Id: {character.CharacterId} Name: {character.Name}";
            var logColor = LogColor.Coral;       

            if (add) {
                Global.Characters.Add(character);
            }

            Global.WriteLog(LogType.Player, logs, logColor);
        }

        private void InsertCharacterData(int index, ref Character character, IConnection connection) {
            var logs = string.Empty;

            if (character.CharacterId <= 0) {
                var database = new DBGameDatabase();
                var dbError = database.Open();

                if (dbError.Number > 0) {
                    Global.WriteLog(LogType.System, $"Failed to insert character Character Id: {character.CharacterId}", LogColor.Red);
                    Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                    Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
                }
                else {
                    // Se o personagem for inserido.
                    if (database.InsertCharacter(character) > 0) {
                        // Obter o Id do personagem.
                        character.CharacterId = database.GetCharacterId(character.AccountId, character.CharacterIndex);

                        new SpCharacterId(index, character.CharacterId).Send(connection);

                        logs = $"Character Id: {character.CharacterId} Name: {character.Name} has been saved";
                        Global.WriteLog(LogType.Player, logs, LogColor.Green);
                    }
                    else {
                        logs = $"Character Id: {character.CharacterId} Name: {character.Name} has not saved";
                        Global.WriteLog(LogType.Player, logs, LogColor.Red);
                    }

                    database.Close();
                }
            }
        }

        private void ReadCharacterData(ref Character character, ref ByteBuffer msg) {
            character.CharacterIndex = msg.ReadByte();
            character.Muted = msg.ReadByte();
            character.Name = msg.ReadString();
            character.Sex = msg.ReadByte();
            character.Classe = msg.ReadByte();
            character.Sprite = msg.ReadInt32();
            character.OriginalSprite = msg.ReadInt32();
            character.Level = msg.ReadInt32();
            character.Experience = msg.ReadInt32();
            character.Access = msg.ReadByte();
            character.PK = msg.ReadByte();
            character.HP = msg.ReadInt32();
            character.MP = msg.ReadInt32();
            character.Strength = msg.ReadInt32();
            character.Endurance = msg.ReadInt32();
            character.Intelligence = msg.ReadInt32();
            character.Agility = msg.ReadInt32();
            character.Vitality = msg.ReadInt32();
            character.Dexterity = msg.ReadInt32();
            character.Points = msg.ReadInt32();
            character.Map = msg.ReadInt32();
            character.X = msg.ReadInt32();
            character.Y = msg.ReadInt32();
            character.Dir = msg.ReadByte();
            character.TutorialState = msg.ReadByte();
            character.TitleID = msg.ReadInt32();
            character.TitleVisibility = msg.ReadByte();
            character.CostumeId = msg.ReadInt32();
            character.BindMap = msg.ReadInt32();
            character.BindX = msg.ReadInt32();
            character.BindY = msg.ReadInt32();
        }
    }
}