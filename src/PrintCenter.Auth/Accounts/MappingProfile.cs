using AutoMapper;

namespace PrintCenter.Auth.Accounts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Models.User, Account>(MemberList.None);
        }
    }
}