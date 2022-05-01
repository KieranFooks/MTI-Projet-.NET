using System;
using System.Collections.Generic;

namespace API.DataAccess
{
    public partial class Tmarket
    {
        public int Id { get; set; }
        public int IdSeller { get; set; }
        public int IdItem { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsSold { get; set; }

        public virtual Titem IdItemNavigation { get; set; } = null!;
        public virtual Tuser IdNavigation { get; set; } = null!;
    }
}
