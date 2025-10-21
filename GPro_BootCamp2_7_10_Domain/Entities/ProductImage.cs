using GPro_BootCamp2_7_10_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public int     ProductId    { get; set; }
        public string  FileName     { get; set; } = "";
        public string? OriginalName { get; set; }
        public long?   Size         { get; set; }
        public bool    IsMain       { get; set; }= false;
        public string? ContentType  { get; set; }

        // Navigation property
        public virtual Product? Product { get; set; } = null!;
    }
}
