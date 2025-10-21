using GPro_BootCamp2_7_10_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }


        public decimal Price { get; set; } = 0;
        public string  Currency { get; set; } = "SAR";
        public int Qty { get; set; } = 1;
        public int ReservedQty { get; set; } = 0;   

        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<ProductImage>? Images { get; set; }= new List<ProductImage>();
    }
}
