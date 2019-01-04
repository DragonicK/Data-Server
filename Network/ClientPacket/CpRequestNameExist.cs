using Data_Server.Util;
using Data_Server.Communication;
using Data_Server.Database;
using Data_Server.Network.ServerPacket;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpRequestExistName : IRecvPacket {

        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var userIndex = msg.ReadInt32();
            var name = msg.ReadString();
            var sex = msg.ReadByte();
            var classe = msg.ReadByte();
            var sprite = msg.ReadInt32();
            var charnum = msg.ReadInt32();

            var database = new DBGameDatabase();
            var dbError = database.Open();

            if (dbError.Number > 0) {
                Global.WriteLog(LogType.System, $"Failed to check if exist Character Name: {name}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
            }
            else {
                var result = database.ExistName(name);
                database.Close();

                new SpExistName(userIndex, result, name, sex, classe, sprite, charnum).Send(connection);
            }
        }
    }
}