using System;
using System.Collections.Generic;

namespace PRN231_G4_ProductManagement_BE.Models
{
    public partial class Store
    {
        public Store()
        {
            Exports = new HashSet<Export>();
        }

        public int Id { get; set; }
        public string? StoreName { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Export> Exports { get; set; }
    }
}
