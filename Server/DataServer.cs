using System;
using System.Collections.Generic;
using Data_Server.Data;
using Data_Server.Util;
using Data_Server.Communication;
using Data_Server.Network;
using Data_Server.Database;

namespace Data_Server.Server {
    public class DataServer : Global {
        public Action<int> UpdateUps;
        public bool ServerRunning { get; set; } = true;

        private int tick;
        private int count;
        private int ups;

        TcpServer Server;
    
        public void InitServer() {
            Server = new TcpServer(45000);
            Server.InitServer();

            OpCode.InitOpCode();

            Characters = new List<Character>();
            Accounts = new List<Account>();
           // LoadAccounts();
        }

        public void ServerLoop() {
            Server.AcceptClient();

            ReceiveSocketData();

            PingConnections();

            CountUps();
        }

        public void StopServer() {
            Server.Stop();
            Connection.Connections.Clear();
        }

        public int SaveCharacters() {
            // -1 = Database fechada.
            var result = -1;
            var saved = 0;
            var management = new CharacterManagement();
            var database = new DBGameDatabase();
            var dbError = database.Open();

            if (dbError.Number > 0) {
                WriteLog(LogType.System, $"Failed to save characters", LogColor.Red);
                WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
            }
            else {
                // 0 = Database aberta ou Sem personagens para salvar.
                result = 0;
     
                for (var n = 0; n < Characters.Count; n++) {

                    if (Characters[n].NeedSave) {
                        management.SaveCharacter(ref database, Characters[n]);
                        saved++;
                    }

                    result = saved;
                }

                database.Close();
            }

            return result;
        }

        public int SerializeCharacters() {
            var management = new CharacterManagement();
            management.CheckFolder();

            for (var n = 0; n < Characters.Count; n++) {
                management.Serialize(Characters[n]);
            }

            return Characters.Count;
        }

        public int DeserializeCharacters() {
            var management = new CharacterManagement();

            Characters = management.DeserializeCharacters();

            return Characters.Count;
        }

        public int ClearPlayers() {
            var count = Characters.Count;

            Characters.Clear();

            return count;
        }

        private void CountUps() {
            if (Environment.TickCount >= tick + 1000) {
                ups = count;
                count = 0;
                tick = Environment.TickCount;

                UpdateUps?.Invoke(ups);
            }
            else {
                count++;
            }
        }

        private void ReceiveSocketData() {
            for (int n = 1; n <= Connection.Connections.Count; n++) {
                if (Connection.Connections.ContainsKey(n)) {
                    Connection.Connections[n].ReceiveData();

                    RemoveWhenNotConnected(n);
                }
            }
        }

        private void RemoveWhenNotConnected(int index) {
            if (!Connection.Connections[index].Connected) {
                string ipAddress = Connection.Connections[index].IpAddress;
                string uniqueKey = Connection.Connections[index].UniqueKey;

                Connection.Remove(index);

                WriteLog(LogType.System, $"{ipAddress} Key {uniqueKey} is disconnected", LogColor.Coral);
            }
        }

        private void PingConnections() {
            for (var n = 1; n <= Connection.Connections.Count; n++) {
                if (Connection.Connections.ContainsKey(n)) {
                    Connection.Connections[n].SendPing();
                }
            }
        }
    }
}