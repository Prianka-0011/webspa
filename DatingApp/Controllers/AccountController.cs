using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
   
    public class AccountController : BaseController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(DataContext context, ITokenService tokenService,IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost("register")]
        
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (await UserExist(register.UserName))
            {
                return BadRequest(register.UserName + " already taken");
            }
            var user=_mapper.Map<AppUser>(register);
            using var hmc = new HMACSHA512();

            user.UserName = register.UserName.ToLower();
            user.Password = hmc.ComputeHash(Encoding.UTF8.GetBytes(register.Password));
            user.PasswordSalt = hmc.Key;
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName=user.UserName,
                Token=_tokenService.CreateToken(user)
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }
        [HttpPost("login")]
        public async Task <ActionResult<UserDto>>Login(LoginDto login)
        {
            var user =await  _context.Users.Include(c=>c.Photos)
                .SingleOrDefaultAsync(c => c.UserName == login.UserName);
            if (user == null)
            {
                return Unauthorized(login.UserName + " Invalid Username");
            }
            var hmc=new HMACSHA512(user.PasswordSalt);
            var compareHmc=hmc.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for (int i = 0; i < compareHmc.Length; i++)
            {
                if (compareHmc[i]!=user.Password[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }
            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl=user.Photos.FirstOrDefault(c=>c.IsMain)?.Url,
                KnownAs=user.KnownAs ,
                Gender=user.Gender 
            };
        }
        private async Task<bool> UserExist(string userName)
        {
          return  await _context.Users.AnyAsync(c => c.UserName == userName.ToLower());
           

           
        }
    }
}
