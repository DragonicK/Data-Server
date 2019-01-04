using System.Threading.Tasks;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Database;
using Data_Server.Communication;
using Data_Server.Server;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpRequestSaveCharacter : IRecvPacket {
        public async void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var characterId = msg.ReadInt32();

            var character = Global.FindCharacterById(characterId);

            if (character != null) {
                var result = await Task.Run(() =>
                    SaveCharacter(character)
                );

                var logs = $"Character Name {character.Name} Character Id {character.CharacterId}";
                var logColor = LogColor.Green;

                if (result > 0) {
                    logs += " is now saved";
                }
                else {
                    logs += " is not saved";
                    logColor = LogColor.Red;
                }

                Global.WriteLog(LogType.Player, logs, logColor);
            }
        }

        private int SaveCharacter(Character character) {
            var result = 0;
            var management = new CharacterManagement();
            var database = new DBGameDatabase();
            var dbError = database.Open();

            if (dbError.Number > 0) {
                Global.WriteLog(LogType.System, $"Failed to save Character Id: {character.CharacterId}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
            }
            else {
                management.SaveCharacter(ref database, character);
                database.Close();

                result = 1;
            }

            return result;
        }
    }
}