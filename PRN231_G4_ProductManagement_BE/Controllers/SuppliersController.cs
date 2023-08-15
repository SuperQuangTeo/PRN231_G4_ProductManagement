using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_G4_ProductManagement_BE.DTO;
using PRN231_G4_ProductManagement_BE.Models;

namespace PRN231_G4_ProductManagement_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private PRN231_Product_ManagementContext _context;
        private IMapper _mapper;
        public SuppliersController(PRN231_Product_ManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllSuppliers")]
        public IActionResult GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            suppliers = _context.Suppliers.ToList();

            return Ok(_mapper.Map<List<SupplierDTO>>(suppliers));
        }

        [HttpGet]
        [Route("GetSupplierById/{id}")]
        public IActionResult GetSupplierById(int id)
        {
            Supplier supplier = _context.Suppliers.Where(x => x.Id == id).SingleOrDefault();

            return Ok(_mapper.Map<SupplierDTO>(supplier));
        }

        [HttpDelete]
        [Route("DeleteSupplierById/{id}")]
        public IActionResult DeleteSupplierById(int id)
        {
            try
            {
                var supplier = _context.Suppliers
                    .Include(c => c.Products)
                    .Where(x => x.Id == id).SingleOrDefault();
                if (supplier == null)
                {
                    return NotFound();
                }

                foreach (var products in supplier.Products.ToList())
                {

                    _context.Products.Remove(products);
                }
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();

                return Ok("delete success");
            }
            catch (Exception e)
            {
                return Conflict("There was an unknown error when performing data deletion.");
            }
        }

        [HttpPost]
        [Route("AddSupplier")]
        public IActionResult AddSupplier([FromBody] SupplierDTO supplier)
        {
            try
            {
                try
                {

                    _context.Suppliers.Add(_mapper.Map<Supplier>(supplier));
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return Ok("add success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateSupplierById")]
        public IActionResult UpdateSupplierById([FromBody] SupplierDTO supplier)
        {
            try
            {
                try
                {
                    _context.Entry<Supplier>(_mapper.Map<Supplier>(supplier)).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return Ok("update success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
