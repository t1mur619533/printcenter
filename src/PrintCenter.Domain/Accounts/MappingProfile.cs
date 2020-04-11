using AutoMapper;

namespace PrintCenter.Domain.Accounts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Models.User, Account>(MemberList.None);
        }
    }
}