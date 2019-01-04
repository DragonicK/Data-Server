using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Server.Network.ServerPacket {
    public sealed class SpPing : SendPacket {
        public ByteBuffer Build() {
            msg.Write(OpCode.SendPacket[GetType()]);
            return msg;
        }
    }
}