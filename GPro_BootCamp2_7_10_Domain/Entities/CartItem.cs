using GPro_BootCamp2_7_10_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }


        // Navigation properties
         public virtual Product Product { get; set; } = null!;
    }
}
