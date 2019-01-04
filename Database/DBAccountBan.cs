using System;
using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Database.MySQL;

namespace Data_Server.Database {
    public sealed partial class DBGameDatabase {

        public bool IsBanned(int accountID) {
            var records = GetBannedRecords(accountID);
            var result = false;

            if (records.Count > 0) {

                foreach (var record in records) {
                    // Se o registro for permanente ou não estiver expirado.
                    if (record.Permanent || IsDateExpired(record.ExpireDate) == Expired.No) {
                        // Indica que o usuário está banido.
                        result = true;
                    }
                }

                // Atualiza a lista quando houver algum registro expirado.
                UpdateBannedRecords(records);
            }

            return result;
        }

        /// <summary>
        /// Compara as datas e retorna o valor do resultado.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private Expired IsDateExpired(DateTime date) {
            if (DateTime.Now.CompareTo(date) == (int)Expired.Yes) {
                return Expired.Yes;
            }

            return Expired.No;
        }

        public void UpdateBanExpiration(int banID, Expired expired) {
            var query = "UPDATE AccountBan SET Expired=@Value WHERE BannedId=@BannedId";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@Value", (byte)expired);
            sqlCommand.AddParameter("@BannedId", banID);
            sqlCommand.Execute();
        }

        /// <summary>
        /// Atualiza os registros de banimento na tabela.
        /// </summary>
        /// <param name="records"></param>
        private void UpdateBannedRecords(List<AccountBan> records) {
            foreach (var record in records) {
                if (IsDateExpired(record.ExpireDate) == Expired.Yes) {
                    UpdateBanExpiration(record.Id, Expired.Yes);
                }
            }
        }

        /// <summary>
        /// Obtém uma lista com os registros banidos do usuário que estão ativos.
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        private List<AccountBan> GetBannedRecords(int accountID) {
            var query = $"SELECT BannedID, ExpireDate, Permanent FROM AccountBan WHERE AccountID=@AccountID AND Expired=@Expired";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@AccountID", accountID);
            sqlCommand.AddParameter("@Expired", (int)Expired.No);

            var sqlReader = sqlCommand.ExecuteReader();

            // Lista de todos os registros de ban que ainda estão ativos.
            var records = new List<AccountBan>();

            while (sqlReader.Read()) {
                var record = new AccountBan {
                    Id = Convert.ToInt32(sqlReader.GetData("BannedID"))
                };

                if (!DBNull.Value.Equals(sqlReader.GetData("ExpireDate"))) {
                    record.ExpireDate = Convert.ToDateTime(sqlReader.GetData("ExpireDate"));
                }

                record.Permanent = Convert.ToBoolean(sqlReader.GetData("Permanent"));

                // Adiciona na lista.
                records.Add(record);
            }

            sqlReader.Close();

            return records;
        }
    }
}