using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Data_Server.Util;
using Data_Server.Communication;
using Data_Server.Network.ServerPacket;

namespace Data_Server.Network {
    public sealed class Connection : IConnection {
        public static Dictionary<int, Connection> Connections { get; set; } = new Dictionary<int, Connection>();

        // Maior indice da lista de usuários.
        public static int HighIndex { get; set; }

        public string UniqueKey { get; set; }
        public bool Connected { get; set; }

        public string IpAddress { get; set; }

        private TcpClient Client;
        private ByteBuffer msg;
        private int pingTick;

        public Connection(TcpClient tcpClient, string ipAddress, string uniqueKey) {
            msg = new ByteBuffer();

            IpAddress = ipAddress;
            UniqueKey = uniqueKey;

            Client = tcpClient;

            Connected = true;
            Add(this);
        }

        public void Disconnect() {
            Client.Close();
            Connected = false;
        }

        public void ReceiveData() {
            if (Client.Client == null) {
                return;
            }

            if (Client.Available > 0) {
                var size = Client.Available;
                byte[] buffer = new byte[size];

                if (Client.Client.Poll(Constants.ReceiveTimeOut, SelectMode.SelectRead)) {
                    try {
                        Client.Client.Receive(buffer, size, SocketFlags.None);
                    }
                    catch (SocketException ex) {
                        Global.WriteLog(LogType.System, $"Receive Data Error: Class {GetType().Name}", LogColor.Red);
                        Global.WriteLog(LogType.System, $"Message: {ex.Message}", LogColor.Red);
                        Disconnect();
                        return;
                    }
                }
                else {
                    Disconnect();
                    return;
                }

                msg.Write(buffer);
                int pLength = 0;

                if (msg.Length() >= 4) {
                    pLength = msg.ReadInt32(false);

                    if (pLength < 0) {
                        return;
                    }
                }

                while (pLength > 0 && pLength <= msg.Length() - 4) {
                    if (pLength <= msg.Length() - 4) {
                        // Remove the first packet (Size of Packet).
                        msg.ReadInt32();
                        // Remove the header.
                        var header = msg.ReadInt32();
                        // Decrease 4 bytes of header.
                        pLength -= 4;

                        if (OpCode.RecvPacket.ContainsKey(header)) {
                            ((IRecvPacket)Activator.CreateInstance(OpCode.RecvPacket[header])).Process(msg.ReadBytes(pLength), this);
                        }
                        else {
                            Global.WriteLog(LogType.System, $"Header: {header} was not found", LogColor.Red);
                        }
                    }

                    pLength = 0;

                    if (msg.Length() >= 4) {
                        pLength = msg.ReadInt32(false);

                        if (pLength < 0) {
                            return;
                        }
                    }
                }

                msg.Trim();
            }
        }

        public void Send(ByteBuffer msg, string className) {
            var buffer = new byte[msg.Length() + 4];
            var values = BitConverter.GetBytes(msg.Length());

            Array.Copy(values, 0, buffer, 0, 4);
            Array.Copy(msg.ToArray(), 0, buffer, 4, msg.Length());

            if (Client.Client.Poll(Constants.SendTimeOut, SelectMode.SelectWrite)) {
                try {
                    Client.Client.Send(buffer, SocketFlags.None);
                }
                catch (SocketException ex) {
                    Global.WriteLog(LogType.System, $"Send Data Error: Class {className}", LogColor.Red);
                    Global.WriteLog(LogType.System, $"Message: {ex.Message}", LogColor.Red);
                    Disconnect();
                }
            }
            else {
                Disconnect();
            }
        }

        public void SendPing() {
            if (Connected) {

                if (Environment.TickCount >= pingTick) {
                    var msg = new SpPing().Build();
                    Send(msg, "SpPing");

                    pingTick = Environment.TickCount + Constants.PingTime;
                }
            }
        }
  
        public static void Remove(int index) {
            if (Connections.ContainsKey(index)) {
                Connections.Remove(index);
            }
        }

        private static void Add(Connection connection) {
            var index = 0;

            if (Connections.Count < HighIndex) {
                // Procura por um slot que não está sendo usado.
                for (var i = 1; i <= HighIndex; i++) {
                    if (!Connections.ContainsKey(i)) {
                        index = i;
                        break;
                    }
                }
            }
            // Caso contrário, adiciona um novo slot.
            else {
                index = ++HighIndex;
            }

            Connections.Add(index, connection);
        }     
    }
} 