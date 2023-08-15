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
    public class CategorysController : ControllerBase
    {
        private PRN231_Product_ManagementContext _context;
        private IMapper _mapper;
        public CategorysController(PRN231_Product_ManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            List<Category> categories = new List<Category>();

            categories = _context.Categories.ToList();

            return Ok(_mapper.Map<List<CategoryDTO>>(categories));
        }

        [HttpGet]
        [Route("GetCategoryById/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            Category category = _context.Categories.Where(x => x.Id == id).SingleOrDefault();

            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpDelete]
        [Route("DeleteCategoryById/{id}")]
        public IActionResult DeleteCategoryById(int id)
        {
            try
            {
                var category = _context.Categories
                    .Include(c => c.Products)
                    .Where(x => x.Id == id).SingleOrDefault();
                if (category == null)
                {
                    return NotFound();
                }

                foreach (var products in category.Products.ToList())
                {

                    _context.Products.Remove(products);
                }
                _context.Categories.Remove(category);
                _context.SaveChanges();

                return Ok("delete success");
            }
            catch (Exception e)
            {
                return Conflict("There was an unknown error when performing data deletion.");
            }
        }

        [HttpPost]
        [Route("AddCategoryById")]
        public IActionResult AddCategoryById([FromBody] CategoryDTO category)
        {
            try
            {
                try
                {

                    _context.Categories.Add(_mapper.Map<Category>(category));
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
        [Route("UpdateCategoryById")]
        public IActionResult UpdateCategoryById([FromBody] CategoryDTO category)
        {
            try
            {
                try
                {
                    _context.Entry<Category>(_mapper.Map<Category>(category)).State = EntityState.Modified;
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
