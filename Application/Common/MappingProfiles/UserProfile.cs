using Application.Common.Dtos;
using Application.Features.User.Commands.RegisterUser;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    { 
        CreateMap<RegisterUserCommand, User>()
            .ForMember(dest 
                    => dest.PasswordHash, 
                opt 
                    => opt.MapFrom(src => src.Password));
        
        CreateMap<User, UserDto>();
    }
}