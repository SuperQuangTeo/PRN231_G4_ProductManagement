namespace PRN231_G4_ProductManagement_BE.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public decimal? Money { get; set; }
    }
}
