using System;
using System.Collections.Generic;

namespace PRN231_G4_ProductManagement_BE.Models
{
    public partial class Product
    {
        public Product()
        {
            Exports = new HashSet<Export>();
            Imports = new HashSet<Import>();
            Spots = new HashSet<Spot>();
        }

        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? ProfitMoney { get; set; }
        public bool? Active { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<Export> Exports { get; set; }
        public virtual ICollection<Import> Imports { get; set; }
        public virtual ICollection<Spot> Spots { get; set; }
    }
}
