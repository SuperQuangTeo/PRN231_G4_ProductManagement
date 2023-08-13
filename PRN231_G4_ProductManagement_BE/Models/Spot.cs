using System;
using System.Collections.Generic;

namespace PRN231_G4_ProductManagement_BE.Models
{
    public partial class Spot
    {
        public int Id { get; set; }
        public string? SpotCode { get; set; }
        public string? Description { get; set; }
        public decimal? ImportPrice { get; set; }
        public int? ImportQuantity { get; set; }
        public DateTime? ImportDate { get; set; }
        public bool? IsActive { get; set; }
        public int? ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
