﻿namespace PRN231_G4_ProductManagement_BE.DTO
{
    public class ImportDTO
    {
        public int Id { get; set; }
        public decimal? ImportPrice { get; set; }
        public int? ImportQuantity { get; set; }
        public DateTime? ImportDate { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public bool? Status { get; set; }

        public virtual ProductDTO? Product { get; set; }
        public virtual UserDTO? User { get; set; }
    }
}
