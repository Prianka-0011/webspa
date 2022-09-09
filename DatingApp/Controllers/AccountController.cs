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
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (await UserExist(register.UserName))
            {
                return BadRequest(register.UserName + " already taken");
            }
            
            using var hmc = new HMACSHA512();
            AppUser user = new AppUser()
            {
                UserName = register.UserName,
                Password = hmc.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmc.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName=user.UserName,
                Token=_tokenService.CreateToken(user)
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
                PhotoUrl=user.Photos.FirstOrDefault(c=>c.IsMain)?.Url
                
            };
        }
        private async Task<bool> UserExist(string userName)
        {
          return  await _context.Users.AnyAsync(c => c.UserName == userName.ToLower());
           

           
        }
    }
}
