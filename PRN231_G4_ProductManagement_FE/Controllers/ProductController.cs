using Microsoft.AspNetCore.Mvc;
using PRN231_G4_ProductManagement_BE.Models;
using PRN231_G4_ProductManagement_FE.Services;
using System.Net;

namespace PRN231_G4_ProductManagement_FE.Controllers
{
    public class ProductController : Controller
    {

        private string rootApiUrl;
        private IConfiguration _configuration;
        public List<Product>? Products { get; set; } = new List<Product>();
        public List<Import>? Imports { get; set; } = new List<Import>();
        public List<Export>? Exports { get; set; } = new List<Export>();

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            rootApiUrl = _configuration.GetSection("ApiUrls")["MyApi"];
        }
        public IActionResult List(string? productName, int? supplierId, int? categoryId, int pageIndex, string? isCheckbox)
        {
            ProductService productService = new ProductService();
            bool? isActive = null;
            if (isCheckbox != null && isCheckbox.Equals("on") || isCheckbox != null && isCheckbox.Equals("true") || isCheckbox != null && isCheckbox.Equals("True"))
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
            ViewData["isActive"] = isCheckbox;
            int totalPage = productService.GetTotalPage(productName, supplierId, categoryId, pageIndex, isActive).Result;
            if (pageIndex <= 0) pageIndex = 1;
            if (pageIndex >= totalPage) pageIndex = totalPage;
            Products = productService.GetProducts(productName, supplierId, categoryId, pageIndex, isActive).Result;
            ViewData["productName"] = productName;
            ViewData["supplierId"] = supplierId;
            ViewData["categoryId"] = categoryId;
            ViewData["isCheckbox"] = isActive;
            ViewData["pre"] = pageIndex - 1;
            ViewData["next"] = pageIndex + 1;
            ViewBag.Suppliers = productService.GetSuppliers().Result;
            ViewBag.Categories = productService.GetCategories().Result;
            return View(Products);
        }

        public IActionResult SpotsDetail(int id)
        {
            ProductService productService = new ProductService();
            List<Spot>? spots = productService.GetSpotsByProductId(id).Result;
            return View(spots);
        }

        public IActionResult AddProduct()
        {
            ProductService productService = new ProductService();
            ViewBag.Suppliers = productService.GetSuppliers().Result;
            ViewBag.Categories = productService.GetCategories().Result;
            return View();
        }

        public IActionResult DoAddProduct(Product newProduct)
        {
            ProductService productService = new ProductService();
            HttpStatusCode add = productService.AddProduct(newProduct).Result;
            if (add == HttpStatusCode.OK)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("AddProduct");
        }

        public IActionResult ProductDetail(int id)
        {
            ProductService productService = new ProductService();
            Product? product = productService.GetProductById(id).Result;
            ViewBag.Suppliers = productService.GetSuppliers().Result;
            ViewBag.Categories = productService.GetCategories().Result;
            return View(product);
        }

        public IActionResult DoUpdateProduct(Product updateProduct, string? isCheckbox)
        {
            ProductService productService = new ProductService();
            if(isCheckbox != null && isCheckbox.Equals("on") || isCheckbox != null && isCheckbox.Equals("true") || isCheckbox != null && isCheckbox.Equals("True"))
            {
                updateProduct.Active = true;
            }
            else
            {
                updateProduct.Active = false;
            }
            HttpStatusCode update = productService.UpdateProduct(updateProduct).Result;
            if (update == HttpStatusCode.OK)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("ProductDetail");
        }

        public IActionResult ExportProductDetail(int id)
        {
            ProductService productService = new ProductService();
            Product? product = productService.GetProductById(id).Result;
            ViewBag.Suppliers = productService.GetSuppliers().Result;
            ViewBag.Categories = productService.GetCategories().Result;
            ViewBag.Stores = productService.GetStores().Result;
            return View(product);
        }

        public IActionResult DoExportProduct(Export exportProduct)
        {
            ProductService productService = new ProductService();
            HttpStatusCode exportStatus = productService.ExportProduct(exportProduct).Result;
            if (exportStatus == HttpStatusCode.OK)
            {
                return RedirectToAction("List");
            }
            else
            {
                ViewData["Message"] = "Export failed. Check all fields again!";
                return RedirectToAction("ExportProductDetail");
            }

        }
        public IActionResult ImportList(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            ProductService productService = new ProductService();
            int totalPage = productService.GetTotalImportsPage(fromDate, toDate, pageIndex).Result;
            if (pageIndex <= 0) pageIndex = 1;
            if (pageIndex >= totalPage) pageIndex = totalPage;
            ViewData["pre"] = pageIndex - 1;
            ViewData["next"] = pageIndex + 1;
            ViewData["fromDate"] = fromDate;
            ViewData["toDate"] = toDate;
            Imports = productService.GetImportList(fromDate, toDate, pageIndex).Result;
            return View(Imports);
        }

        public IActionResult ExportList(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            ProductService productService = new ProductService();
            int totalPage = productService.GetTotalExportsPage(fromDate, toDate, pageIndex).Result;
            if (pageIndex <= 0) pageIndex = 1;
            if (pageIndex >= totalPage) pageIndex = totalPage;
            ViewData["pre"] = pageIndex - 1;
            ViewData["next"] = pageIndex + 1;
            ViewData["fromDate"] = fromDate;
            ViewData["toDate"] = toDate;
            Exports = productService.GetExportList(fromDate, toDate, pageIndex).Result;
            return View(Exports);
        }
    }
}
