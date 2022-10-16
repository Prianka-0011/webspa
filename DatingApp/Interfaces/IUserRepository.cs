using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser appUser);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByNameAsync(string UserName);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<MemberDto> GetMemberAsync(string userName);
    }
}
