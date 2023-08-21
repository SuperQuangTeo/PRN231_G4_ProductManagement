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

        List<Spot> GetSpotsByProductId(int productId);

        void AddImport(Import import);

        void AddSpot(Spot spot);

        void AddExport(Export export);
        List<Import> GetAllImports(DateTime? fromDate, DateTime? toDate, int pageIndex);
        List<Export> GetAllExports(DateTime? fromDate, DateTime? toDate, int pageIndex);

        int GetTotalImportPage(DateTime? fromDate, DateTime? toDate, int pageIndex);
        int GetTotalExportPage(DateTime? fromDate, DateTime? toDate, int pageIndex);
    }
}
