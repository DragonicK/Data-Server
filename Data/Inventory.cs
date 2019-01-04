using System;

namespace Data_Server.Data {
    [Serializable()]
    public struct Inventory {
        public int Index { get; set; }
        public int ItemID { get; set; }
        public int ItemValue { get; set; }
        public int ItemLevel { get; set; }
        public byte ItemBound { get; set; }
    }
}