using System;

namespace Data_Server.Data {
    [Serializable()]
    public struct SpellBuff {
        public int Index { get; set; }
        public int ID { get; set; }
        public int Duration { get; set; }
        public int Level { get; set; }
    }
}