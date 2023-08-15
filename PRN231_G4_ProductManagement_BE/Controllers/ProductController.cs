﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_G4_ProductManagement_BE.DTO;
using PRN231_G4_ProductManagement_BE.IService;
using PRN231_G4_ProductManagement_BE.Models;
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

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            ProductDTO product = _mapper.Map<ProductDTO>(_productService.GetProductById(id));
            return Ok(product);
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
    }
}