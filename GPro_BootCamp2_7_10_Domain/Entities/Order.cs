using GPro_BootCamp2_7_10_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Domain.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public string OrderNo { get; set; } = "";
        public OrderStatus Status { get; set; }= OrderStatus.Pending;
        public decimal Total { get; set; }  = 0;
        public string Currency { get; set; } = "SAR";
         // Navigation properties
        public virtual ICollection<OrderItem>? Items { get; set; } = new List<OrderItem>();
    }

    public enum OrderStatus
    {
        Pending     =0,
        Returned    =1,
        Cancelled   =2,
        Processing  =3,
        Shipped     =4,
        Delivered   =5,
       
    }
}
