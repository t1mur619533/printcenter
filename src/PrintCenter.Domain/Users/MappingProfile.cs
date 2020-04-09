using AutoMapper;

namespace PrintCenter.Domain.Users
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.UserDto, Data.Models.User>(MemberList.None);
            CreateMap<Data.Models.User, User>(MemberList.None);
        }
    }
}