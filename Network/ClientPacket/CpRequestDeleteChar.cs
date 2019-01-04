using Data_Server.Communication;
using Data_Server.Util;
using Data_Server.Database;
using Data_Server.Network.ServerPacket;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpRequestDeleteChar : IRecvPacket {

        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var userIndex = msg.ReadInt32();
            var accountID = msg.ReadInt32();
            var characterIndex = msg.ReadInt32();

            if (accountID > 0 && userIndex > 0) {
                var database = new DBGameDatabase();
                var dbError = database.Open();

                if (dbError.Number > 0) {
                    Global.WriteLog(LogType.System, $"Failed to delete character from Account Id: {accountID}", LogColor.Red);
                    Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                    Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
                } 
                else {
                    database.DeleteCharacter(accountID, characterIndex);

                    var character = Global.FindCharacterByIndexAndAccountId(accountID, characterIndex);
                    if (character != null) {
                        Global.Characters.Remove(character);
                    }

                    var characters = database.GetCharacterList(accountID);
                    new SpCharacterList(userIndex, characters).Send(connection);

                    database.Close();
                }
            }
        }
    }
}