using AutoMapper;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Technologies
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Models.Technology, Technology>(MemberList.None);
            CreateMap<Technology, Data.Models.Technology>(MemberList.None);
        }
    }
}