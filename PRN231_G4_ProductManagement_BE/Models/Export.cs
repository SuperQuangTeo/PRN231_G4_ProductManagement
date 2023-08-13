using System;
using System.Collections.Generic;

namespace PRN231_G4_ProductManagement_BE.Models
{
    public partial class Export
    {
        public int Id { get; set; }
        public decimal? ExportPrice { get; set; }
        public int? ExportQuantity { get; set; }
        public DateTime? ExportDate { get; set; }
        public int? ProductId { get; set; }
        public int? StoreId { get; set; }
        public int? UserId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Store? Store { get; set; }
        public virtual User? User { get; set; }
    }
}
