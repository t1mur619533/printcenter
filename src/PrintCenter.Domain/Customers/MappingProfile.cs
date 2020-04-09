using AutoMapper;

namespace PrintCenter.Domain.Customers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.CustomerDto, Data.Models.User>(MemberList.None);
            CreateMap<Data.Models.Customer, Customer>(MemberList.None);
        }
    }
}