using AutoMapper;
using Intro.Models.DTO;
using Intro.Models.Model;

namespace Intro.WebApi
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // User to UserDTO and vice versa
            CreateMap<User, UserDTO>();
            CreateMap<CreateEditUserDTO, User>().ForMember(dest => dest.IsActive, source => source.MapFrom(_ => true));
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.UserTitle, source => source.MapFrom(x => new UserTitle { Description = x.UserTitleDescription }))
                .ForMember(dest => dest.UserType, source => source.MapFrom(x => new UserType { Description = x.UserTypeDescription, Code = x.UserTypeCode }));

            // UserTitle to UserTitleDTO and vice versa
            CreateMap<UserTitle, UserTitleDTO>();
            CreateMap<UserTitleDTO, UserTitle>();

            // UserType to UserTypeDTO and vice versa
            CreateMap<UsertTypeDTO, UserType>();
            CreateMap<UserType, UsertTypeDTO>();
        }
    }
}