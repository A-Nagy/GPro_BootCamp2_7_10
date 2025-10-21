using GPro_BootCamp2_7_10_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }  

        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();

    }
}
