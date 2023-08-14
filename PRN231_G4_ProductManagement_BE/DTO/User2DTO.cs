using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.DTO
{
    public class User2DTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public decimal? Money { get; set; }

        public virtual ICollection<RoleDTO> Roles { get; set; }
    }
}
