using System;
using System.Collections.Generic;

namespace API.DataAccess
{
    public partial class Tuser
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Money { get; set; }
        public string Password { get; set; } = null!;

        public virtual Tmarket Tmarket { get; set; } = null!;
    }
}
