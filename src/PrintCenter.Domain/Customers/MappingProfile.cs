using AutoMapper;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Customers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.Command, Data.Models.Customer>(MemberList.None);
            CreateMap<Edit.Command, Data.Models.Customer>(MemberList.None);
            
            CreateMap<Data.Models.Customer, Customer>(MemberList.None);
        }
    }
}