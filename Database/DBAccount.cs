using System;
using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Database.Interface;
using Data_Server.Database.MySQL;

namespace Data_Server.Database {
    public sealed partial class DBGameDatabase {
        private IDBConnection sqlConnection = null;
        private IDBFactory factory = null;

        public DBGameDatabase() {
            factory = new DBFactory();
        }

        public DBError Open() {
            sqlConnection = factory.GetConnection();
            return sqlConnection.Open();
        }

        public void Close() {
            sqlConnection.Close();
        }

        public bool IsOpen() {
            return sqlConnection.IsOpen();
        }

        public Account GetAccountData(string username) {
            var query = "SELECT AccountID, Password, UserGroup, ServiceID FROM AccountData WHERE Username=@Username";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.AddParameter("@Username", username);
            sqlCommand.SetCommand(query);

            var sqlReader = sqlCommand.ExecuteReader();
            var pData = new Account();

            if (sqlReader.Read()) {
                pData.AccountID  = (int)sqlReader.GetData("AccountID");
                pData.Password = (string)sqlReader.GetData("Password");
                pData.UserGroup = (int)sqlReader.GetData("UserGroup");
                pData.ServiceID = Convert.ToByte(sqlReader.GetData("ServiceID"));
                pData.AccountName = username;
            }

            sqlReader.Close();

            return pData;
        }

        public List<Account> GetAccountList() {
            var query = "SELECT AccountID, Username, Password, UserGroup, ServiceID FROM AccountData";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);

            var sqlReader = sqlCommand.ExecuteReader();
            var list = new List<Account>();

            while (sqlReader.Read()) {
                var account = new Account() {
                    AccountID = (int)sqlReader.GetData("AccountID"),
                    AccountName = (string)sqlReader.GetData("Username"),
                    Password = (string)sqlReader.GetData("Password"),
                    UserGroup = (int)sqlReader.GetData("UserGroup"),
                    ServiceID = Convert.ToByte(sqlReader.GetData("ServiceID")),
                };

                list.Add(account);
            }

            sqlReader.Close();

            return list;
        }
    }
}