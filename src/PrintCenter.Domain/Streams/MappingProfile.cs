using AutoMapper;

namespace PrintCenter.Domain.Streams
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Shared.Stream, Data.Models.Stream>(MemberList.None);
            CreateMap<Data.Models.Stream, Shared.Stream>(MemberList.None);
        }
    }
}
