using Application.Common.Dtos;
using Application.Features.Role.Commands.CreateRole;
using Application.Features.Role.Commands.UpdateRole;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<CreateRoleCommand, Role>();
        CreateMap<UpdateRoleCommand, Role>();
    }
}