using DatingApp.DTOs;
using DatingApp.Extensions;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class LikesController : BaseController
    {
        private readonly ILikeRepository likeRepository;
        private readonly IUserRepository userRepository;
        public LikesController(ILikeRepository likeRepository, IUserRepository userRepository)
        {
            this.likeRepository = likeRepository;
            this.userRepository = userRepository;
        }
        [HttpPost("{userName}")]
        public async Task<ActionResult>AddLike(string userName)
        {
            int sourceUserId =User.GetUserId();
            var likedUser = await userRepository.GetUserByNameAsync(userName);
            var sourceUser = await likeRepository.GetUserWithLikes(sourceUserId);
            if(likedUser == null)
            {
                return NotFound();
            }
            if(sourceUser.UserName == userName)
            {
                return BadRequest("You can not like your self");
            }
            var userLike = await likeRepository.GetUserLike(sourceUserId,likedUser.Id);
            if(userLike != null)
            {
                return BadRequest("You already like this user");
            }
            userLike = new Entities.UserLike
            {
                SourceUserId = sourceUserId,
                LikeUserId = likedUser.Id,
            };
            sourceUser.LikedUsers.Add(userLike);
            if(await userRepository.SaveAllAsync())
            {
                return Ok();
            }
            return BadRequest("Faild to add like user");
        }
        [HttpGet]
        public async Task<ActionResult<PagedList<LikeDto>>>GetUserLikes([FromQuery]LikesParams likesParams)
        {
            likesParams.UserId= User.GetUserId();
            var users = await likeRepository.GetUserLikes(likesParams);
            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage,
                users.PageSize, users.TotalCount, users.TotalPage));
            return Ok(users);
        }
        
    }
}
