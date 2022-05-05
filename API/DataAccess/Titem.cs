using System;
using System.Collections.Generic;

namespace API.DataAccess
{
    public partial class Titem
    {
        public Titem()
        {
            Tmarkets = new HashSet<Tmarket>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Tmarket> Tmarkets { get; set; }
    }
}
