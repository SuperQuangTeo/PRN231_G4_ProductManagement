namespace PRN231_G4_ProductManagement_BE.DTO
{
    public class ExportDTO
    {
        public int Id { get; set; }
        public decimal? ExportPrice { get; set; }
        public int? ExportQuantity { get; set; }
        public DateTime? ExportDate { get; set; }
        public int? ProductId { get; set; }
        public int? StoreId { get; set; }
        public int? UserId { get; set; }
        public bool? Status { get; set; }

        public virtual ProductDTO? Product { get; set; }
        public virtual StoreDTO? Store { get; set; }
        public virtual UserDTO? User { get; set; }
    }
}
