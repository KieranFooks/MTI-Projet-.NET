using System;
using System.Collections.Generic;

namespace API.DataAccess
{
    public partial class Item
    {
        public Item()
        {
            Markets = new HashSet<Market>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Market> Markets { get; set; }
    }
}
