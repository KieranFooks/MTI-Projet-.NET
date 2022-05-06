using System;
using System.Collections.Generic;

namespace API.DataAccess
{
    public partial class Tuser
    {
        public Tuser()
        {
            Tinventories = new HashSet<Tinventory>();
            Tmarkets = new HashSet<Tmarket>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Money { get; set; }
        public string Password { get; set; } = null!;

        public virtual ICollection<Tinventory> Tinventories { get; set; }
        public virtual ICollection<Tmarket> Tmarkets { get; set; }
    }
}
