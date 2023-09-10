using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Extensions;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DataContext _context;
        public LikeRepository(DataContext context)
        {
            _context= context;
        }
        public async Task<UserLike> GetUserLike(int sourceUserId, int likeUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, likeUserId);
        }

        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();
            if(likesParams.Predicate == "liked")
            {
                likes=likes.Where(l=>l.SourceUserId == likesParams.UserId);
                users = likes.Select(like => like.LikedUser);
            }
            if (likesParams.Predicate == "likedBy")
            {
                likes = likes.Where(l => l.LikeUserId == likesParams.UserId);
                users = likes.Select(like => like.SourceUser);
            }
            var likedUser = users.Select(user => new LikeDto
            {
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(c => c.IsMain).Url,
                City = user.City,
                Id = user.Id,
            });
            return await PagedList<LikeDto>.CreatAsync(likedUser, likesParams.PageNumber, likesParams.PageSize);
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users.Include(c=>c.LikedUsers)
                .FirstOrDefaultAsync(c=>c.Id == userId);
        }
    }
}
