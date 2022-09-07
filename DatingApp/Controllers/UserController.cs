using AutoMapper;
using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Extensions;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public UserController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
        }
        [HttpGet]
       
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
        [HttpGet("{userName}", Name = "GetUserByName")]
        
        public async Task<ActionResult<MemberDto>> GetUserByName(string userName)
        {
            //var user = await _userRepository.GetUserByNameAsync(userName);
            var user = await _userRepository.GetMemberAsync(userName);
            //var returnUser = _mapper.Map<MemberDto>(user);
            return Ok(user);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto member)
        {

            var user=await _userRepository.GetUserByNameAsync(User.GetUserName());
            _mapper.Map(member, user);
            _userRepository.Update(user);
            if (await _userRepository.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Faild to update user");
        }
       
        [HttpPost("addphoto")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user=await _userRepository.GetUserByNameAsync(User.GetUserName());
            var result = await _photoService.AddPhotoAsync(file);
            if (result.Error !=null)
            {
                return BadRequest(result.Error.Message);
            }
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if (user.Photos.Count==0)
            {
                photo.IsMain = true;
            }
            user.Photos.Add(photo);
            if (await _userRepository.SaveAllAsync())
            {
                return CreatedAtRoute("GetUserByName", new {userName=user.UserName}, _mapper.Map<PhotoDto>(photo));
                //return _mapper.Map< PhotoDto>(photo);
            }
            return BadRequest("Problem Adding Photo");
        }
    }
}
