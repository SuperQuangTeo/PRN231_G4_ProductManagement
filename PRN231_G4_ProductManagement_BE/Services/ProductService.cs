using Microsoft.EntityFrameworkCore;
using PRN231_G4_ProductManagement_BE.DTO;
using PRN231_G4_ProductManagement_BE.IService;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.Services
{
    public class ProductService : IProductService
    {
        PRN231_Product_ManagementContext _context;
        int pageSize = 15;
        public ProductService(PRN231_Product_ManagementContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts(string? productName, int? supplierId, int? categoryId, int pageIndex, bool? isActive)
        {
            List<Product> products = _context.Products.Include(x => x.Supplier).Include(x => x.Category).Include(x => x.Unit).ToList();
            if(productName != null) products = products.Where(p => p.ProductName.ToLower().Contains(productName.ToLower())).ToList();
            if(supplierId != null) products = products.Where(p => p.SupplierId == supplierId).ToList();
            if(categoryId != null) products = products.Where(p => p.CategoryId == categoryId).ToList();
            if(isActive == false) products = products.Where(p => p.Active == false).ToList();
            if(isActive == true) products = products.Where(p => p.Active == true).ToList();

            return products.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Product getProduct = _context.Products.Include(x => x.Supplier).Include(x => x.Unit).Include(x => x.Category).FirstOrDefault(x => x.Id == product.Id);
            if(getProduct != null)
            {
                getProduct.ProductName = product.ProductName;
                getProduct.Description = product.Description;
                getProduct.Quantity = product.Quantity;
                getProduct.Price = product.Price;
                getProduct.ProfitMoney = product.ProfitMoney;
                getProduct.SupplierId = product.SupplierId;
                getProduct.CategoryId = product.CategoryId;
                getProduct.UnitId = product.UnitId;

                _context.SaveChanges();
            }
            
        }

        public Product? GetProductById(int productId)
        {
            return _context.Products.Include(x => x.Supplier).Include(x => x.Category).Include(x => x.Unit).FirstOrDefault(x => x.Id == productId);
        }
    }
}
