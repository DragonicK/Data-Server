using System.Threading.Tasks;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Database;
using Data_Server.Communication;
using Data_Server.Network.ServerPacket;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpRequestCharacterData : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var userIndex = msg.ReadInt32();
            var accountID = msg.ReadInt32();
            var characterIndex = msg.ReadInt32();

            SendCharacterData(userIndex, accountID, characterIndex, connection);
        }

        private async void SendCharacterData(int userIndex, int accountID, int characterIndex, IConnection connection) {
            var character = await Task.Run(() =>
                GetCharacterAsync(accountID, characterIndex)        
            );

            // Quando nulo, retorna.
            if (character == null) {
                return;
            }

            // Define o personagem como online.
            character.Connected = true;

            // A partir do momento que o personagem é carregado no servidor, as atualizações de informações serão constantes.
            // Caso seja necessário fechar o programa, a lista de personagens será salva.
            character.NeedSave = true;

            new SpCharacterData(userIndex, character).Send(connection);
            new SpCharacterSpell(userIndex, character.Spell).Send(connection);
            new SpCharacterInventory(userIndex, character.Inventory).Send(connection);
            new SpCharacterEquipment(userIndex, character.Equipment).Send(connection);
            new SpCharacterWarehouse(userIndex, character.Warehouse).Send(connection);
            new SpCharacterHotbar(userIndex, character.Hotbar).Send(connection);
            new SpCharacterTitle(userIndex, character.Title).Send(connection);
            new SpCharacterVariable(userIndex, character.Variable).Send(connection);
            new SpCharacterSpellBuff(userIndex, character.SpellBuff).Send(connection);
            new SpCharacterCurrency(userIndex, character.Currency).Send(connection);   
            new SpCharacterCraft(userIndex, character.Craft).Send(connection);
            new SpCharacterAchievement(userIndex, character.Achievement).Send(connection);

            Global.WriteLog(LogType.Player, $"Request Data Character Id {character.CharacterId} Character Name {character.Name} ", LogColor.Coral);
        }

        private Character GetCharacterAsync(int accountId, int characterIndex) {
            var character = Global.FindCharacterByIndexAndAccountId(accountId, characterIndex);

            if (character == null) {     
                // Quando não encontrado, procura no banco de dados.
                var database = new DBGameDatabase();
                var dbError = database.Open();

                if (dbError.Number > 0) {
                    Global.WriteLog(LogType.System, $"Failed to get character from database Account Id {accountId}", LogColor.Red);
                    Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                    Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
                } 
                else {
                    character = database.GetCharacter(accountId, characterIndex);
                    character.Spell = database.GetCharacterSpells(character.CharacterId);
                    character.Inventory = database.GetCharacterItems(character.CharacterId, InventoryType.Inventory);
                    character.Equipment = database.GetCharacterItems(character.CharacterId, InventoryType.Equipment);
                    character.Warehouse = database.GetCharacterItems(character.CharacterId, InventoryType.Warehouse);
                    character.Hotbar = database.GetCharacterHotbar(character.CharacterId);
                    character.Title = database.GetCharacterTitle(character.CharacterId);
                    character.Variable = database.GetCharacterVariable(character.CharacterId);
                    character.SpellBuff = database.GetCharacterSpellBuff(character.CharacterId);
                    character.Currency = database.GetCharacterCurrency(character.CharacterId);
                    character.Craft = database.GetCharacterCraft(character.CharacterId);
                    character.Achievement = database.GetCharacterAchievements(character.CharacterId);

                    database.Close();

                    // Adiciona o personagem quando válido.
                    Global.Characters.Add(character);
                }
            }

            return character;
        }
    }
}