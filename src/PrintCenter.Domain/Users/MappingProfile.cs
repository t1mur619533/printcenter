using AutoMapper;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Users
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Models.User, User>(MemberList.None);
            CreateMap<Data.Models.User, User>(MemberList.None);
        }
    }
}