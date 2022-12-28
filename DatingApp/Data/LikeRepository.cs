using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using System.Collections.Generic;
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
        public Task<UserLike> GetUserLike(int sourceUserId, int likeUserId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<LikeDto>> GetUserLikes(int predicate, int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<AppUser> GetUserWithLikes(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
