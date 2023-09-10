using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.Interfaces
{
    public interface ILikeRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId ,int likeUserId);
        Task<AppUser> GetUserWithLikes( int userId);
        Task<PagedList<LikeDto>>GetUserLikes(LikesParams likesParams);
        //new comment 
    }
}
