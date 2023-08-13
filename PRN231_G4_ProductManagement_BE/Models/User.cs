using System;
using System.Collections.Generic;

namespace PRN231_G4_ProductManagement_BE.Models
{
    public partial class User
    {
        public User()
        {
            Exports = new HashSet<Export>();
            Imports = new HashSet<Import>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public decimal? Money { get; set; }

        public virtual ICollection<Export> Exports { get; set; }
        public virtual ICollection<Import> Imports { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
