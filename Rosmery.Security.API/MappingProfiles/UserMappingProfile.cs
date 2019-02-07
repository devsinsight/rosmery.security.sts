using AutoMapper;
using Rosmery.Security.API.Commands;
using Rosmery.Security.API.Models;
using Rosmery.Security.Identity.Models;

namespace Rosmery.Security.API.MappingProfiles
{
    public class UserMappingProfile : Profile 
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(y => y.Id));
            CreateMap<CreateUserCommand, User>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.UserId));
            CreateMap<UpdateUserCommand, User>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.UserId));
        }
    }
}
