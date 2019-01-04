using System.Collections.Generic;
using Data_Server.Util;
using Data_Server.Data;
using Data_Server.Database;
using System.Linq;

namespace Data_Server.Communication {
    public class Global {
        public static Log PlayerLogs { get; set; }
        public static Log SystemLogs { get; set; }

        /// <summary>
        /// Personagens recebidos pelo servidor da crystalshire.
        /// </summary>
        public static List<Character> Characters { get; set; }
        
        /// <summary>
        /// Cache de usuários carregados do banco de dados.
        /// </summary>
        public static List<Account> Accounts { get; set; }

        public static Character FindCharacterById(int characterId) {
            var result = from character in Characters
                         where character.CharacterId == characterId
                         select character;

            return result.FirstOrDefault();                  
        }

        public static Character FindCharacterByName(string name) {
            var result = from character in Characters
                         where string.Compare(character.Name, name, true) == 0
                         select character;

            return result.FirstOrDefault();
        }

        public static Character FindCharacterByIndexAndAccountId(int accountId, int characterIndex) {
            var result = from character in Characters
                         where (character.AccountId == accountId) && (character.CharacterIndex == characterIndex)
                         select character;

            return result.FirstOrDefault();
        }

        public static Account FindAccountByName(string name) {
            var result = from account in Accounts
                         where string.Compare(account.AccountName, name, true) == 0
                         select account;

            return result.FirstOrDefault();
        }

        public static void LoadAccounts() {
            var database = new DBGameDatabase();
            var dbError = database.Open();

            if (dbError.Number > 0) {
                WriteLog(LogType.System, $"Failed to load accounts", LogColor.Red);
                WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
            }
            else {
                Accounts = database.GetAccountList();
                database.Close();

                WriteLog(LogType.System, $"{Accounts.Count} accounts loaded", LogColor.Green);
            }
        }

        public static void WriteLog(LogType type, string text, LogColor color) {
            switch(type) {
                case LogType.Player:
                    PlayerLogs.Write(text, color);
                    break;
                case LogType.System:
                    SystemLogs.Write(text, color);
                    break;
            }
        }
    }
}