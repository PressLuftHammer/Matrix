using System;
using System.Collections.Generic;

namespace Matrix.Models
{
    public partial class Object
    {
        public Object()
        {
            ObjectStatus = new HashSet<ObjectStatus>();
            ObjectTrack = new HashSet<ObjectTrack>();
        }

        public int Id { get; set; }
        public int ModuleId { get; set; }
        public DateTime CrtDate { get; set; }

        public Module Module { get; set; }
        public ICollection<ObjectStatus> ObjectStatus { get; set; }
        public ICollection<ObjectTrack> ObjectTrack { get; set; }
    }
}
