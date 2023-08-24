using Microsoft.EntityFrameworkCore;
using PRN231_G4_ProductManagement_BE.DTO;
using PRN231_G4_ProductManagement_BE.IService;
using PRN231_G4_ProductManagement_BE.Models;
using System.Text;

namespace PRN231_G4_ProductManagement_BE.Services
{
    public class ProductService : IProductService
    {
        PRN231_Product_ManagementContext _context;
        int pageSize = 7;
        public ProductService(PRN231_Product_ManagementContext context)
        {
            _context = context;
        }

        public ProductService()
        {
        }
        public List<Product> allProduct()
        {
            using (var context1 = new PRN231_Product_ManagementContext())
            {
                List<Product> products = context1.Products.Include(x => x.Supplier).Include(x => x.Category).Include(x => x.Unit).ToList();
                return products;
            }
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
            if(product.Id != 0)
            {
                Import import = new Import();
                import.ImportPrice = product.Price;
                import.ImportQuantity = product.Quantity;
                import.ImportDate = DateTime.Now;
                import.ProductId = product.Id;
                import.Status = true;
                AddImport(import);

                Spot spot = new Spot();
                spot.ProductId = product.Id;
                spot.ImportQuantity = product.Quantity;
                spot.ImportDate = DateTime.Now;
                spot.ImportPrice = product.Price;
                spot.SpotCode = GenerateRandomString(5);
                spot.IsActive = true;
                AddSpot(spot);
            }
        }

        public void UpdateProduct(Product product)
        {
            Product getProduct = _context.Products.Include(x => x.Supplier).Include(x => x.Unit).Include(x => x.Category).FirstOrDefault(x => x.Id == product.Id);
            if(getProduct != null)
            {   
                getProduct.ProductName = product.ProductName;
                getProduct.Description = product.Description;
                getProduct.Price = product.Price;
                getProduct.ProfitMoney = product.ProfitMoney;
                if (product.Active == true)
                {
                    getProduct.Active = true;
                }
                else getProduct.Active = false;
                getProduct.SupplierId = product.SupplierId;
                getProduct.CategoryId = product.CategoryId;
                getProduct.UnitId = product.UnitId;

                if (product.Quantity > 0)
                {
                    getProduct.Quantity = getProduct.Quantity + product.Quantity;
                    Import import = new Import();
                    import.ImportPrice = product.Price;
                    import.ImportQuantity = product.Quantity;
                    import.ImportDate = DateTime.Now;
                    import.ProductId = product.Id;
                    import.Status = true;
                    AddImport(import);

                    Spot spot = new Spot();
                    spot.ProductId = product.Id;
                    spot.ImportQuantity = product.Quantity;
                    spot.ImportDate = DateTime.Now;
                    spot.ImportPrice = product.Price;
                    spot.SpotCode = GenerateRandomString(5);
                    spot.IsActive = true;
                    AddSpot(spot);
                }
                else
                {
                }

                _context.SaveChanges();
            }
            
        }

        public Product? GetProductById(int productId)
        {
            return _context.Products.Include(x => x.Supplier).Include(x => x.Category).Include(x => x.Unit).FirstOrDefault(x => x.Id == productId);
        }

        public int GetTotalProductPage(string? productName, int? supplierId, int? categoryId, int pageIndex, bool? isActive)
        {
            List<Product> products = _context.Products.Include(x => x.Supplier).Include(x => x.Category).Include(x => x.Unit).ToList();
            if (productName != null) products = products.Where(p => p.ProductName.ToLower().Contains(productName.ToLower())).ToList();
            if (supplierId != null) products = products.Where(p => p.SupplierId == supplierId).ToList();
            if (categoryId != null) products = products.Where(p => p.CategoryId == categoryId).ToList();
            if (isActive == false) products = products.Where(p => p.Active == false).ToList();
            if (isActive == true) products = products.Where(p => p.Active == true).ToList();
            int totalPage = products.Count % pageSize == 0 ? products.Count / pageSize : products.Count / pageSize + 1;
            return totalPage;
        }

        public List<Spot> GetSpotsByProductId(int productId)
        {
            return _context.Spots.Where(x => x.ProductId == productId).OrderByDescending(x => x.ImportDate).ToList();
        }

        public void AddImport(Import import)
        {
            _context.Imports.Add(import);
            _context.SaveChanges();
        }

        public void AddSpot(Spot spot)
        {
            _context.Spots.Add(spot);
            _context.SaveChanges();
        }

        public string GenerateRandomString(int length)
        {

            Random Random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = Random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }

        public void AddExport(Export export)
        {
            Product? product = _context.Products.FirstOrDefault(x => x.Id == export.ProductId);
            if(product != null)
            {
                List<Spot> spots = GetSpotsByProductIdAcrs(product.Id);
                if(product.Quantity - export.ExportQuantity >= 0)
                {
                    int? exportQuantity = export.ExportQuantity;
                    foreach (var s in spots)
                    {
                        int quantityToSubtract = Math.Min((int)s.ImportQuantity, (int)exportQuantity);
                        s.ImportQuantity -= quantityToSubtract;
                        if (s.ImportQuantity == 0) UpdateSpotStatus(s.Id);
                        exportQuantity -= quantityToSubtract;

                        UpdateSpotQuantity(s.Id, s.ImportQuantity);
                    }

                    UpdateProductQuantity(product.Id, export.ExportQuantity);
                    _context.Exports.Add(export);
                    _context.SaveChanges();
                }
            }   
        }

        private void UpdateSpotQuantity(int spotId, int? quantity)
        {
            Spot? spot = _context.Spots.FirstOrDefault(x => x.Id == spotId);
            if(spot != null)
            {
                spot.ImportQuantity = quantity;
                _context.SaveChanges();
            }
            
        }

        private void UpdateSpotStatus(int spotId)
        {
            Spot? spot = _context.Spots.FirstOrDefault(x => x.Id == spotId);
            if (spot != null)
            {
                spot.IsActive = false;
                _context.SaveChanges();
            }

        }

        private void UpdateProductQuantity(int productId, int? quantity)
        {
            Product? product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if(product != null)
            {
                product.Quantity = product.Quantity - quantity;
                _context.SaveChanges();
            }
            
        }

        private List<Spot> GetSpotsByProductIdAcrs(int productId)
        {
            return _context.Spots.Where(x => x.ProductId == productId).OrderBy(x => x.ImportDate).ToList();
        }

        public List<Import> GetAllImports(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            List<Import> imports = _context.Imports.Include(x => x.Product).ThenInclude(x => x.Supplier)
                .Include(x => x.Product).ThenInclude(x => x.Category).Include(x => x.User).ToList();

            if (fromDate != null) imports = imports.Where(x => x.ImportDate >= fromDate).ToList();
            if (toDate != null) imports = imports.Where(x => x.ImportDate <= toDate).ToList();

            return imports.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Export> GetAllExports(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            List<Export> exports = _context.Exports.Include(x => x.Product).ThenInclude(x => x.Supplier)
                .Include(x => x.Product).ThenInclude(x => x.Category).Include(x => x.Store).Include(x => x.User).ToList();

            if(fromDate != null) exports = exports.Where(x => x.ExportDate >= fromDate).ToList();
            if(toDate != null) exports = exports.Where(x => x.ExportDate <= toDate).ToList();

            return exports.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetTotalImportPage(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            List<Import> imports = _context.Imports.Include(x => x.Product).Include(x => x.User).ToList();

            if (fromDate != null) imports = imports.Where(x => x.ImportDate >= fromDate).ToList();
            if (toDate != null) imports = imports.Where(x => x.ImportDate <= toDate).ToList();
            int totalPage = imports.Count % pageSize == 0 ? imports.Count / pageSize : imports.Count / pageSize + 1;
            return totalPage;
        }

        public int GetTotalExportPage(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            List<Export> exports = _context.Exports.Include(x => x.Product).Include(x => x.Store).Include(x => x.User).ToList();

            if (fromDate != null) exports = exports.Where(x => x.ExportDate >= fromDate).ToList();
            if (toDate != null) exports = exports.Where(x => x.ExportDate <= toDate).ToList();
            int totalPage = exports.Count % pageSize == 0 ? exports.Count / pageSize : exports.Count / pageSize + 1;
            return totalPage;
        }
    }
}
