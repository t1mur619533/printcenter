using AutoMapper;

namespace PrintCenter.Domain.Materials
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Models.Material, Shared.Material>(MemberList.None);
            CreateMap<Shared.Material, Data.Models.Material>(MemberList.None);
        }
    }
}
