using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPro_BootCamp2_7_10_Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
       
        public bool IsDeleted  { get; set; } = false;
        public int? UserDelted { get; set; } = null;
        public DateTime? DeletedAt { get; set; }

        public DateTime CreatedAt  { get; set; }= DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public byte[] RowVersion   { get; set; } = Array.Empty<byte>();
    }
}
