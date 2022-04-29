using System;
using System.Collections.Generic;

namespace API.DataAccess
{
    public partial class Inventory
    {
        public int IdUser { get; set; }
        public int IdItem { get; set; }
        public int Quantity { get; set; }

        public virtual Item IdItemNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
