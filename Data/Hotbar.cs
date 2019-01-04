using System;

namespace Data_Server.Data {
    [Serializable()]
    public struct Hotbar {
        public int Index { get; set; }
        public int Value { get; set; }
        public byte Type { get; set; }
    }
}