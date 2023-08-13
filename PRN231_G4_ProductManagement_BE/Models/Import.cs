using System;
using System.Collections.Generic;

namespace PRN231_G4_ProductManagement_BE.Models
{
    public partial class Import
    {
        public int Id { get; set; }
        public decimal? ImportPrice { get; set; }
        public int? ImportQuantity { get; set; }
        public DateTime? ImportDate { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
