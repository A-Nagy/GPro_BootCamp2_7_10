using GPro_BootCamp2_7_10_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Domain.Entities
{
    public class Supplier :BaseEntity
    {
        public string Name { get; set; } = "";
        public string? Email { get; set; }
        public string? Phone{ get; set; }
        public string? Address { get; set; }


        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
