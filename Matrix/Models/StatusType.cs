using System;
using System.Collections.Generic;

namespace Matrix.Models
{
    public partial class StatusType
    {
        public StatusType()
        {
            ObjectStatus = new HashSet<ObjectStatus>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ObjectStatus> ObjectStatus { get; set; }
    }
}
