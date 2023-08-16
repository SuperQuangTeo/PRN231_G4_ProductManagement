using Microsoft.AspNetCore.Mvc;
using PRN231_G4_ProductManagement_BE.Models;
using PRN231_G4_ProductManagement_FE.Services;

namespace PRN231_G4_ProductManagement_FE.Controllers
{
    public class ProductController : Controller
    {

        private string rootApiUrl;
        private IConfiguration _configuration;
        public List<Product>? Products { get; set; } = new List<Product>();

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            rootApiUrl = _configuration.GetSection("ApiUrls")["MyApi"];
        }
        public IActionResult List(string? productName, int? supplierId, int? categoryId, int pageIndex, string? isCheckbox)
        {
            bool? isActive = null;
            ProductService productService = new ProductService();
            if (isCheckbox != null && isCheckbox.Equals("true"))
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
            ViewData["isActive"] = isCheckbox;
            Products = productService.GetProducts(productName, supplierId, categoryId, pageIndex, isActive).Result;
            ViewData["supplierId"] = supplierId;
            ViewData["categoryId"] = categoryId;
            ViewBag.Suppliers = productService.GetSuppliers().Result;
            ViewBag.Categories = productService.GetCategories().Result;
            return View(Products);
        }
    }
}
