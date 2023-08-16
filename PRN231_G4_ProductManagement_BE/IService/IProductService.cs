using PRN231_G4_ProductManagement_BE.DTO;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.IService
{
    public interface IProductService
    {
        List<Product> GetAllProducts(string? productName, int? supplierId, int? categoryId, int pageIndex, bool? isActive);

        void AddProduct(Product product);
        void UpdateProduct(Product product);
        Product? GetProductById(int productId);

        int GetTotalProductPage(string? productName, int? supplierId, int? categoryId, int pageIndex, bool? isActive);
    }
}
