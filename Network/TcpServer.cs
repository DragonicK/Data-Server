using System.Net;
using System.Linq;
using System.Net.Sockets;
using Data_Server.Communication;
using Data_Server.Util;

namespace Data_Server.Network {
    public sealed class TcpServer {
        public int Port { get; set; }

        private bool accept;
        private TcpListener server;

        public TcpServer() { }

        public TcpServer(int port) {
            Port = port;
        }

        public void InitServer() {
            server = new TcpListener(IPAddress.Any, Port);
            server.Start();

            accept = true;
        }

        public void AcceptClient() {
            if (accept) {
                if (server.Pending()) {
                    var client = server.AcceptTcpClient();
                    var ipAddress = client.Client.RemoteEndPoint.ToString();

                    if (IsValidIpAddress(ipAddress)) {
                        var uniqueKey = new KeyGenerator().GetUniqueKey();

                        new Connection(client, ipAddress, uniqueKey);
                        Global.WriteLog(LogType.System, $"{ipAddress} Key {uniqueKey} is connected", LogColor.Coral);
                    }
                    else {
                        client.Close();
                        Global.WriteLog(LogType.System, $"Hacking Attempt: Invalid IpAddress {ipAddress}", LogColor.Blue);
                    }
                }
            }
        }

        public void Stop() {
            accept = false;
            server.Stop();   
        }

        private bool IsValidIpAddress(string ipAddress) {
            const int IpAddressArraySplit = 4;
            const int Last = 3;

            if (string.IsNullOrWhiteSpace(ipAddress) || string.IsNullOrEmpty(ipAddress)) {
                return false;
            }

            var values = ipAddress.Split('.');
            if (values.Length != IpAddressArraySplit) {
                return false;
            }

            // Retira o número da porta.
            values[Last] = values[Last].Remove(values[Last].IndexOf(':'));

            return values.All(r => byte.TryParse(r, out byte parsing));
        }
    }
}