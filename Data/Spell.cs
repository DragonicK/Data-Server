using System;

namespace Data_Server.Data {
    [Serializable()]
    public struct Spell {
        public int Index { get; set; }
        public int ID { get; set; }
        public int Uses { get; set; }
        public int Level { get; set; }
    }
}