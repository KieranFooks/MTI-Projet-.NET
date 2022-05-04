using System;
using System.Collections.Generic;

namespace API.DataAccess
{
    public partial class Tinventory
    {
        public int IdUser { get; set; }
        public int IdItem { get; set; }
        public int Quantity { get; set; }

        public virtual Titem IdItemNavigation { get; set; } = null!;
        public virtual Tuser IdUserNavigation { get; set; } = null!;
    }
}
