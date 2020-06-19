using System;
using System.Collections.Generic;

namespace Matrix.Models
{
    public partial class ObjectTrack
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public DateTime CrtDate { get; set; }

        public Object Object { get; set; }
    }
}
