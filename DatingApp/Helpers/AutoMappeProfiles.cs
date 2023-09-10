

using AutoMapper;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Extensions;
using System.Linq;

namespace DatingApp.Helpers
{
    public class AutoMappeProfiles:Profile
    {
        public AutoMappeProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest=>dest.PhotoUrl,opt=>opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url))
                .ForMember(dest=>dest.Age,opt=>opt.MapFrom(src=>src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();

            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<Message, MessageDto>()
                .ForMember(d=>d.SenderPhotoUrl, o=>o.MapFrom(s=>s.Sender.Photos.FirstOrDefault(c=>c.IsMain).Url))
                .ForMember(d=>d.SenderPhotoUrl,o=>o.MapFrom(c=>c.Recepient.Photos.FirstOrDefault(c=>c.IsMain).Url));
               
        }
    }
}
