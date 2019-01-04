using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Database;
using Data_Server.Communication;
 
namespace Data_Server.Server {
    public sealed class CharacterManagement {
        public void SaveCharacter(ref DBGameDatabase database, Character character) {
            database.UpdateCharacter(character);
            database.UpdateAchievement(character.CharacterId, character.Achievement);
            database.UpdateCraft(character.CharacterId, character.Craft);
            database.UpdateCurrency(character.CharacterId, character.Currency);
            database.UpdateHotbar(character.CharacterId, character.Hotbar);
            database.UpdateSpell(character.CharacterId, character.Spell);
            database.UpdateSpellBuff(character.CharacterId, character.SpellBuff);
            database.UpdateTitle(character.CharacterId, character.Title);
            database.UpdateEquipment(character.CharacterId, character.Equipment);
            database.UpdateInventory(character.CharacterId, character.Inventory);
            database.UpdateWarehouse(character.CharacterId, character.Warehouse);
            database.UpdateVariable(character.CharacterId, character.Variable);
        }

        public void Serialize(Character character) {
            var binaryFormatter = new BinaryFormatter();
            using (var file = new FileStream($"./Serialized/{character.Name.Trim()}.bin", FileMode.Create, FileAccess.Write)) {
                binaryFormatter.Serialize(file, character);
                file.Close();
            };
        }

        public Character Deserialize(string name) {
            var character = new Character();
            var binaryFormatter = new BinaryFormatter();

            using (var file = new FileStream(name, FileMode.Open, FileAccess.Read)) {
                character = binaryFormatter.Deserialize(file) as Character;
                file.Close();
            };

            return character;
        }

        public List<Character> DeserializeCharacters() {
            CheckFolder();

            var list = new List<Character>();
            var files = Directory.GetFiles("./Serialized/");

            foreach(var file in files) {
                var character = Deserialize(file);
                
                // Muda para verdadeiro, pois o personagem pode não ter sido atualizado no banco por algum erro.
                character.NeedSave = true;
                list.Add(character);

                Global.WriteLog(LogType.System, $"Character Id: {character.CharacterId} Name:{character.Name} has been deserialized", LogColor.Coral);
            }

            return list;
        }

        public void CheckFolder() {
            if (!Directory.Exists("./Serialized")) {
                Directory.CreateDirectory("./Serialized");
            }
        }
    }
}