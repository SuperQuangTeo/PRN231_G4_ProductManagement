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
    public class StoresController : ControllerBase
    {
        private PRN231_Product_ManagementContext _context;
        private IMapper _mapper;
        public StoresController(PRN231_Product_ManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllStores")]
        public IActionResult GetAllStores()
        {
            List<Store> stores = new List<Store>();

            stores = _context.Stores.ToList();
            
            return Ok(_mapper.Map<List<StoreDTO>>(stores));
        }

        [HttpGet]
        [Route("GetStoreById/{id}")]
        public IActionResult GetStoreById(int id)
        {
            Store store = _context.Stores.Where(x => x.Id == id).SingleOrDefault();

            return Ok(_mapper.Map<StoreDTO>(store));
        }

        [HttpDelete]
        [Route("DeleteStoreById/{id}")]
        public IActionResult DeleteStoreById(int id)
        {
            try
            {
                var store = _context.Stores
                    .Include(c => c.Exports)
                    .Where(x => x.Id == id).SingleOrDefault();
                if (store == null)
                {
                    return NotFound();
                }


                foreach (var exports in store.Exports.ToList())
                {

                    _context.Exports.Remove(exports);
                }
                _context.Stores.Remove(store);
                _context.SaveChanges();

                return Ok("delete success");
            }
            catch (Exception e)
            {
                return Conflict("There was an unknown error when performing data deletion.");
            }
        }

        [HttpPost]
        [Route("AddStoreById")]
        public IActionResult AddStoreById([FromBody] StoreDTO store)
        {
            try
            {
                try
                {

                    _context.Stores.Add(_mapper.Map<Store>(store));
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
        [Route("UpdateStoreById")]
        public IActionResult UpdateStoreById([FromBody] StoreDTO store)
        {
            try
            {
                try
                {
                    _context.Entry<Store>(_mapper.Map<Store>(store)).State = EntityState.Modified;
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
