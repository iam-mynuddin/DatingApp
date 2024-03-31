using API.Data;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var objList=await _context.Users.ToListAsync();
            return objList;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var obj =await _context.Users.FindAsync(id);
            if (obj == null)
            {
                return NotFound("User id"+id.ToString()+" is not found");
            }
            return obj;
        }
       
    }
}
