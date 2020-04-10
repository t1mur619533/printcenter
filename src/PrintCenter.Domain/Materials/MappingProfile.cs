using AutoMapper;

namespace PrintCenter.Domain.Materials
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.MaterialDto, Data.Models.Material>(MemberList.None);

            CreateMap<Data.Models.Material, Material>(MemberList.None);
        }
    }
}
