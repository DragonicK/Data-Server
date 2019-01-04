using System;
using System.Collections.Generic;

namespace Data_Server.Data {
    [Serializable()]
    public sealed class Craft {
        public byte Type { get; set; }
        public byte Index { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public List<int> Recipes { get; set; }
    }
}