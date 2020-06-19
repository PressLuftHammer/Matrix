using System;
using System.Collections.Generic;

namespace Matrix.Models
{
    public partial class Module
    {
        public Module()
        {
            Object = new HashSet<Object>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }

        public ICollection<Object> Object { get; set; }
    }
}
