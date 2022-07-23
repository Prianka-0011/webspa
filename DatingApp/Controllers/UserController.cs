using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<MemberDto>>> GetUsers()
        {
            var users =await _userRepository.GetMembersAsync();
            //var returnUser=_mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(users);
        }

        //[HttpGet("{id}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<AppUser>> GetUser(int id)
        //{
        //    var user = await _userRepository.GetUserByIdAsync(id);
        //    return Ok(user);
        //}
        [HttpGet("{userName}")]
        [AllowAnonymous]
        public async Task<ActionResult<MemberDto>> GetUserByName(string userName)
        {
            //var user = await _userRepository.GetUserByNameAsync(userName);
            var user = await _userRepository.GetMemberAsync(userName);
            //var returnUser = _mapper.Map<MemberDto>(user);
            return Ok(user);
        }

    }
}
