using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_G4_ProductManagement_BE.DTO;
using PRN231_G4_ProductManagement_BE.Models;
using System.IO;

namespace PRN231_G4_ProductManagement_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private PRN231_Product_ManagementContext _context ;
        private IMapper _mapper;
        public UsersController(PRN231_Product_ManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            List<User> users = new List<User>();
            using (_context = new PRN231_Product_ManagementContext())
            {
                users = _context.Users.ToList();
            }
            return Ok(users);
        }

        [HttpGet]
        [Route("getUsersByEmail")]

        public IActionResult getUsersByEmail(string? email)
        {
            try
            {
                if (email == null) return NoContent();
                User user = new User();
                user = _context.Users.Where(x => x.Email.Equals(email)).Include(x => x.Roles).SingleOrDefault();
                User2DTO user2Dto = _mapper.Map<User2DTO>(user);
                if (user2Dto == null) return NoContent();
                return Ok(user2Dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateUsers")]
        public IActionResult UpdateUsers([FromBody] UserDTO user)
        {
            try
            {
                try
                {
                    _context.Entry<User>(_mapper.Map<User>(user)).State = EntityState.Modified;
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

        [HttpPost]
        [Route("CreateUsers")]
        public IActionResult CreateUsers([FromBody] UserDTO user)
        {
          
            try
            {
                try
                {

                    _context.Users.Add(_mapper.Map<User>(user));
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return Ok("Create success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteUserById/{id}")]
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                var users = _context.Users
                    .Include(c => c.Imports)
                    .Include(c => c.Exports)
                    .FirstOrDefault(x => x.Id == id);
                if (users == null)
                {
                    return NotFound();
                }


                foreach (var imports in users.Imports.ToList())
                {
                    foreach (var exports in users.Exports.ToList())
                    {
                        _context.Exports.RemoveRange(exports);

                    }
                    _context.Imports.RemoveRange(imports);
                }
                _context.Users.Remove(users);

                _context.SaveChanges();

                return Ok("delete success");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
