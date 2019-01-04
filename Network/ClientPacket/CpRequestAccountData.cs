using System;
using Data_Server.Util;
using Data_Server.Database;
using Data_Server.Communication;
using Data_Server.Network.ServerPacket;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpRequestAccountData : IRecvPacket  {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var userIndex = msg.ReadInt32();
            var username = msg.ReadString();

            if (userIndex == 0 || username.Length <= 0) {
                return;
            }

            CheckAccount(userIndex, username, connection);
        }

        private void CheckAccount(int userIndex, string username, IConnection connection) {
            var database = new DBGameDatabase();
            var dbError = database.Open();

            if (dbError.Number > 0) {
                Global.WriteLog(LogType.System, $"Failed to check Account: {username}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);

                return;
            } 
   
            // Procura pelo usuário na lista de cache.
            var account = Global.FindAccountByName(username);

            // Quando nulo, procura no banco de dados.
            if (account == null) {
                
                account = database.GetAccountData(username);

                // Quando o usuário é válido, adiciona na lista.
                if (account.AccountID > 0) {
                    Global.Accounts.Add(account);
                }
            }

            account.Banned = Convert.ToByte(database.IsBanned(account.AccountID));

            var characters = database.GetCharacterList(account.AccountID);
            database.Close();

            // Envia os personagens.
            new SpAccountData(userIndex, account, characters).Send(connection);

            Global.WriteLog(LogType.Player, $"Request account data: {account.AccountName}", LogColor.Black);
            Global.WriteLog(LogType.Player, $"UserIndex: {userIndex} Id: {account.AccountID} Status: {account.Banned} UserGroup: {account.UserGroup}", LogColor.Black);

            foreach (var character in characters) {
                Global.WriteLog(LogType.Player, $"Account Id: {account.AccountID} Character Id: {character.CharacterID} Name: {character.Name} ", LogColor.Black);
            }
        }
    }
}