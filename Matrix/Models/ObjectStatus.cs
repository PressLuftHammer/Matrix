using System;
using System.Collections.Generic;

namespace Matrix.Models
{
    public partial class ObjectStatus
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public int StatusTypeId { get; set; }
        public DateTime CrtDate { get; set; }

        public Object Object { get; set; }
        public StatusType StatusType { get; set; }
    }
}
