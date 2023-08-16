using PRN231_G4_ProductManagement_BE;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_FE.Services
{
    internal class ProductService : BaseService
    {
        public async Task<List<Product>?> GetProducts(string? productName, int? supplierId, int? categoryId, int pageIndex, bool? isActive)
        {
            List<Product>? products = await GetData<List<Product>>("product/list?" + "productName=" + productName 
                + "&supplierId=" + supplierId + "&categoryId=" + categoryId + "&pageIndex=" + pageIndex + "&isActive=" + isActive);
            return products;
        }

        public async Task<List<Category>?> GetCategories()
        {
            List<Category>? categories = await GetData<List<Category>>("Categorys/GetAllCategories");
            return categories;
        }

        public async Task<List<Supplier>?> GetSuppliers()
        {
            List<Supplier>? suppliers = await GetData<List<Supplier>>("Suppliers/GetAllSuppliers");
            return suppliers;
        }

        public async Task<int> GetTotalPage(string? productName, int? supplierId, int? categoryId, int pageIndex, bool? isActive)
        {
            int totalPage = await GetData<int>("product/list/totalPage?" + "productName=" + productName
                + "&supplierId=" + supplierId + "&categoryId=" + categoryId + "&pageIndex=" + pageIndex + "&isActive=" + isActive);
            return totalPage;
        }
    }
}
