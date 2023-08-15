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
    public class UnitsController : ControllerBase
    {
        private PRN231_Product_ManagementContext _context;
        private IMapper _mapper;
        public UnitsController(PRN231_Product_ManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllUnits")]
        public IActionResult GetAllUnits()
        {
            List<Unit> units = new List<Unit>();

            units = _context.Units.ToList();

            return Ok(_mapper.Map<List<UnitDTO>>(units));
        }

        [HttpGet]
        [Route("GetUnitById/{id}")]
        public IActionResult GetUnitById(int id)
        {
            Unit unit = _context.Units.Where(x => x.Id == id).SingleOrDefault();

            return Ok(_mapper.Map<UnitDTO>(unit));
        }

        [HttpDelete]
        [Route("DeleteUnitById/{id}")]
        public IActionResult DeleteUnitById(int id)
        {
            try
            {
                var unit = _context.Units
                    .Include(c => c.Products)
                    .Where(x => x.Id == id).SingleOrDefault();
                if (unit == null)
                {
                    return NotFound();
                }

                foreach (var products in unit.Products.ToList())
                {

                    _context.Products.Remove(products);
                }
                _context.Units.Remove(unit);
                _context.SaveChanges();

                return Ok("delete success");
            }
            catch (Exception e)
            {
                return Conflict("There was an unknown error when performing data deletion.");
            }
        }

        [HttpPost]
        [Route("AddUnit")]
        public IActionResult AddUnit([FromBody] UnitDTO unit)
        {
            try
            {
                try
                {

                    _context.Units.Add(_mapper.Map<Unit>(unit));
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
        [Route("UpdateUnitById")]
        public IActionResult UpdateUnitById([FromBody] UnitDTO unit)
        {
            try
            {
                try
                {
                    _context.Entry<Unit>(_mapper.Map<Unit>(unit)).State = EntityState.Modified;
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
