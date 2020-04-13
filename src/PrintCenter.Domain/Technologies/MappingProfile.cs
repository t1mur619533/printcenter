using AutoMapper;

namespace PrintCenter.Domain.Technologies
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.TechnologyDto, Data.Models.Technology>(MemberList.None);
            CreateMap<Edit.TechnologyDto, Data.Models.Technology>(MemberList.None);

            CreateMap<Data.Models.Technology, Technology>(MemberList.None);
        }
    }
}