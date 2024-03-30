using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("register")] //POST api/account/register
        public async Task<ActionResult<AppUser>>Register([FromBody]RegisterDTO dto) //FromBody is not required because api controller class knows where to look
        {
            dto.Username=Regex.Replace(dto.Username, @"\s+", ""); 
            if (await UserExists(dto.Username)) return BadRequest(dto.Username+" already exists");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = dto.Username.ToLower(),
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt=hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDTO dto) //FromBody is not required because api controller class knows where to look
        {
            var userFromDb=await _context.Users.SingleOrDefaultAsync(u=>u.UserName==dto.Username.ToLower());
            if(userFromDb==null) return Unauthorized("Incorrect username");

            using var hmac = new HMACSHA512(userFromDb.PasswordSalt);

            var currentComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            int i = 0;
            foreach(var item in currentComputedHash)
            {
                if (item != userFromDb.PasswordHash[i]) return Unauthorized("Incorrect password");
                i++;
            }

            return userFromDb;
        }
        [ApiExplorerSettings(IgnoreApi = true)]//Only to avoid error with swagger
        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
        }
    }
}
