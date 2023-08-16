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
            ProductService productService = new ProductService();
            bool? isActive = null;
            if (isCheckbox != null && isCheckbox.Equals("true"))
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
            ViewData["isActive"] = isCheckbox;
            int totalPage = productService.GetTotalPage(productName, supplierId, categoryId, pageIndex, isActive).Result;
            if(pageIndex <= 0) pageIndex = 1;
            if (pageIndex >= totalPage) pageIndex = totalPage;
            Products = productService.GetProducts(productName, supplierId, categoryId, pageIndex, isActive).Result;
            ViewData["productName"] = productName;
            ViewData["supplierId"] = supplierId;
            ViewData["categoryId"] = categoryId;
            ViewData["isCheckbox"] = isCheckbox;
            ViewData["pre"] = pageIndex - 1;
            ViewData["next"] = pageIndex + 1;
            ViewBag.Suppliers = productService.GetSuppliers().Result;
            ViewBag.Categories = productService.GetCategories().Result;
            return View(Products);
        }
    }
}
