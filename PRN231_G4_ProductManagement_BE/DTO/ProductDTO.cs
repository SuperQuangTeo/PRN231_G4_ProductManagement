using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? ProfitMoney { get; set; }
        public bool? Active { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public int? UnitId { get; set; }

        public virtual CategoryDTO? Category { get; set; }
        public virtual SupplierDTO? Supplier { get; set; }
        public virtual UnitDTO? Unit { get; set; }
    }
}
