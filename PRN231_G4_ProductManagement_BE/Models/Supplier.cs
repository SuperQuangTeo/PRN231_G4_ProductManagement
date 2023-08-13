using System;
using System.Collections.Generic;

namespace PRN231_G4_ProductManagement_BE.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? SupplierName { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
