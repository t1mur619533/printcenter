using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PrintCenter.Shared;

namespace PrintCenter.Domain.Users
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Models.User, User>(MemberList.None)
                .ForMember(_ => _.Role, cfg => cfg.MapFrom(_ => _.Role.ToString()))
                .ForMember(_ => _.TechnologyNames, cfg => cfg.MapFrom(_ => _.Technologies.Select(t => t.Name)));
            CreateMap<Data.Models.User, UserDetail>(MemberList.None)
                .ForMember(_ => _.Role, cfg => cfg.MapFrom(_ => _.Role.ToString()))
                .ForMember(detail => detail.Technologies, cfg => cfg.MapFrom(user => user.Technologies));
        }
    }
}