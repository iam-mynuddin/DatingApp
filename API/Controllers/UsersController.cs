using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
		private readonly IMapper _mapper;

		public UsersController(DataContext context,IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
		}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var objList=await _context.Users.Include(u=>u.Photos).ToListAsync();
            var objListToReturn =_mapper.Map<IEnumerable<MemberDto>>(objList);
            return Ok(objListToReturn);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var obj =await _context.Users.FindAsync(id);
            if (obj == null)
            {
                return NotFound("User id"+id.ToString()+" is not found");
            }
            return _mapper.Map<MemberDto>(obj);;
		}
       
    }
}
