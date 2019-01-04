using System;

namespace Data_Server.Data {
    [Serializable()]
    public struct Currency {
        public int Index { get; set; }
        public int Value { get; set; }
    }
}