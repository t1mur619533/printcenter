using AutoMapper;

namespace PrintCenter.Domain.Users
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Create.Command, Data.Models.User>(MemberList.None);
            CreateMap<Data.Models.User, User>(MemberList.None);
            CreateMap<Data.Models.User, User>(MemberList.None);
            CreateMap<Data.Models.User, UserEnvelope>(MemberList.None)
                .ForMember(envelope => envelope.User, cfg => cfg.MapFrom(user => user))
                .ForMember(envelope => envelope.Technologies, cfg => cfg.MapFrom(user => user.Technologies));
        }
    }
}