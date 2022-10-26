using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context,IMapper mapper)
        {
            _context= context;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string userName)
        {
            var member = await _context.Users
               .Where(c => c.UserName == userName)
               .ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            return member;
            
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query =  _context.Users
                //.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking()
                .AsQueryable();
            query = query.Where(c => c.UserName != userParams.CurrentUsername);
            query = query.Where(c => c.Gender == userParams.Gender);
            var minDob=DateTime.Today.AddYears(-userParams.MaxAge-1);
            var maxDob=DateTime.Today.AddYears(-userParams.MinAge);
            query = query.Where(c => c.DateOfBirth >=minDob && c.DateOfBirth<=maxDob);

            return await PagedList<MemberDto>.CreatAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber,userParams.PageSize);

        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByNameAsync(string userName)
        {
            return await _context.Users.Include(c => c.Photos)
                .SingleOrDefaultAsync(c => c.UserName == userName);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.Include(c=>c.Photos).ToListAsync();
        }

       public async Task<bool> SaveAllAsync()
        {
           return await _context.SaveChangesAsync()>0;
        }

        public void Update(AppUser appUser)
        {
            _context.Entry(appUser).State = EntityState.Modified;
        }
    }
}
