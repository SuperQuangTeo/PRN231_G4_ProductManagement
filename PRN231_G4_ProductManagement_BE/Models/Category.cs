using System;
using System.Collections.Generic;

namespace PRN231_G4_ProductManagement_BE.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? CategoryType { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
