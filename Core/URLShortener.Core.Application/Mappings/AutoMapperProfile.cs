using AutoMapper;
using Project.Core.Application.Commons;
using Project.Core.Application.DTOs;
using Project.Core.Application.Features.Users.Commands;
using Project.Core.Domain.Entities;
using Project.Core.Domain.Enums;
using URLShortener.Core.Application.DTOs;
using URLShortener.Core.Application.Features.Link.Commands;
using URLShortener.Core.Domain.Entities;

namespace Project.Core.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GetUserRoleDto, UserRole>();
            CreateMap<UserRole, GetUserRoleDto>();
            CreateMap<SetUserDto, User>();
            CreateMap<User, GetUserDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == Gender.Male ? "Male" : "Female"));
            CreateMap<CreateUserCommand.Request, User>();
            CreateMap<UpdateUserCommand.Request, User>();
            CreateMap<CreateLinkCommand.Request, LinkEntity>();
            CreateMap<LinkEntity, GetLinkDto>();

            CreateMap(typeof(Pagination<>), typeof(GetPaginationDto<>));

        }
    }
}
