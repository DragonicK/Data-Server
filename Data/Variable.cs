using System;

namespace Data_Server.Data {
    [Serializable()]
    public struct Variable {
        public int ID { get; set; }
        public int Value { get; set; }
    }
}