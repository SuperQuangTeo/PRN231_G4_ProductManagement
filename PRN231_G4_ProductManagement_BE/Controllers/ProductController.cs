using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_G4_ProductManagement_BE.DTO;
using PRN231_G4_ProductManagement_BE.IService;
using PRN231_G4_ProductManagement_BE.Models;
using PRN231_G4_ProductManagement_BE.Services;

namespace PRN231_G4_ProductManagement_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private PRN231_Product_ManagementContext _context;
        private IMapper _mapper;
        private IProductService _productService;
        public ProductController(PRN231_Product_ManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _productService = new ProductService(_context);
        }
        [HttpGet("list")]
        public IActionResult GetProducts(string? productName, int? supplierId, int? categoryId, int pageIndex, bool? isActive)
        {
            try
            {
                List<Product>? products = _productService.GetAllProducts(productName, supplierId, categoryId, pageIndex, isActive);
                List<ProductDTO> productsDTO = _mapper.Map<List<ProductDTO>>(products);
                return Ok(productsDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
        [HttpGet("list/totalPage")]
        public IActionResult GetTotalProductsPage(string? productName, int? supplierId, int? categoryId, int pageIndex, bool? isActive)
        {
            try
            {
                int totalPage = _productService.GetTotalProductPage(productName, supplierId, categoryId, pageIndex, isActive);
                return Ok(totalPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            Product? product = _productService.GetProductById(id);
            ProductDTO productDTO = _mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductDTO product)
        {
            Product newProduct = new Product();
            newProduct.ProductName = product.ProductName;
            newProduct.Description = product.Description;
            newProduct.SupplierId = product.SupplierId;
            newProduct.CategoryId = product.CategoryId;
            newProduct.Price = product.Price;
            newProduct.Quantity = product.Quantity;
            newProduct.ProfitMoney = product.ProfitMoney;
            newProduct.Active = product.Active;
            newProduct.UnitId = product.UnitId;
            _productService.AddProduct(newProduct);
            return Ok(product);
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] ProductDTO product)
        {
            Product newProduct = new Product();
            newProduct.Id = product.Id;
            newProduct.ProductName = product.ProductName;
            newProduct.Description = product.Description;
            newProduct.SupplierId = product.SupplierId;
            newProduct.CategoryId = product.CategoryId;
            newProduct.Price = product.Price;
            newProduct.Quantity = product.Quantity;
            newProduct.ProfitMoney = product.ProfitMoney;
            newProduct.Active = product.Active;
            newProduct.UnitId = product.UnitId;
            _productService.UpdateProduct(newProduct);
            return Ok(product);
        }

        [HttpGet("spots/{productId}")]
        public IActionResult GetSpotsByProductId(int productId)
        {
            try
            {
                List<Spot> spots = _productService.GetSpotsByProductId(productId);
                return Ok(spots);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost("ExportProduct")]
        public IActionResult ExportProduct([FromBody] Export productExport)
        {
            Export export = new Export();
            try
            {
                export.ExportPrice = productExport.ExportPrice;
                export.ExportQuantity = productExport.ExportQuantity;
                export.ExportDate = DateTime.Now;
                export.StoreId = productExport.StoreId;
                export.ProductId = productExport.ProductId;
                export.Status = true;
                _productService.AddExport(export);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("ImportList")]
        public IActionResult GetImports(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            try
            {
                List<ImportDTO> importDTOs = _mapper.Map<List<ImportDTO>>(_productService.GetAllImports(fromDate, toDate, pageIndex));
                return Ok(importDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            
        }

        [HttpGet("ExportList")]
        public IActionResult GetExports(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            try
            {
                List<ExportDTO> exportDTOs = _mapper.Map<List<ExportDTO>>(_productService.GetAllExports(fromDate, toDate, pageIndex));
                return Ok(exportDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpGet("ImportList/totalPage")]
        public IActionResult GetTotalImportsPage(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            try
            {
                int totalPage = _productService.GetTotalImportPage(fromDate, toDate, pageIndex);
                return Ok(totalPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpGet("ExportList/totalPage")]
        public IActionResult GetTotalExportsPage(DateTime? fromDate, DateTime? toDate, int pageIndex)
        {
            try
            {
                int totalPage = _productService.GetTotalExportPage(fromDate, toDate, pageIndex);
                return Ok(totalPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
    }
}
