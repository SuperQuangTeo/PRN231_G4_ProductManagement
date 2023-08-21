using PRN231_G4_ProductManagement_BE;
using PRN231_G4_ProductManagement_BE.Models;
using System.Net;

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

        public async Task<List<Spot>?> GetSpotsByProductId(int productId)
        {
            List<Spot>? spots = await GetData<List<Spot>>("product/spots/" + productId);
            return spots;
        }

        public async Task<Product?> GetProductById(int productId)
        {
            Product ? product = await GetData<Product>("product/" + productId);
            return product;
        }

        public async Task<HttpStatusCode> UpdateProduct(Product product)
        {
            HttpStatusCode updateProduct = await PutData<Product>("product", product);
            return updateProduct;
        }

        public async Task<HttpStatusCode> AddProduct(Product product)
        {
            HttpStatusCode addProduct = await PushData<Product>("product", product);
            return addProduct;
        }

        public async Task<List<Store>?> GetStores()
        {
            List<Store>? stores = await GetData<List<Store>>("Stores/GetAllStores");
            return stores;
        }

        public async Task<HttpStatusCode> ExportProduct(Export export)
        {
            HttpStatusCode updateProduct = await PushData<Export>("product/ExportProduct", export);
            return updateProduct;
        }

        public async Task<List<Import>?> GetImportList(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            List<Import>? imports = await GetData<List<Import>>("product/ImportList?" + "fromDate=" + fromDate
                + "&toDate=" + toDate + "&pageIndex=" + pageIndex);
            return imports;
        }

        public async Task<List<Export>?> GetExportList(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            List<Export>? exports = await GetData<List<Export>>("product/ExportList?" + "fromDate=" + fromDate
                + "&toDate=" + toDate + "&pageIndex=" + pageIndex);
            return exports;
        }

        public async Task<int> GetTotalImportsPage(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            int totalPage = await GetData<int>("product/ImportList/totalPage?" + "fromDate=" + fromDate
                + "&toDate=" + toDate + "&pageIndex=" + pageIndex);
            return totalPage;
        }

        public async Task<int> GetTotalExportsPage(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            int totalPage = await GetData<int>("product/ExportList/totalPage?" + "fromDate=" + fromDate
                + "&toDate=" + toDate + "&pageIndex=" + pageIndex);
            return totalPage;
        }
    }
}
