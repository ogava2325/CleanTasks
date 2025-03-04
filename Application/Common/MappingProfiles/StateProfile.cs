using Application.Common.Dtos;
using Application.Features.State.Commands.CreateState;
using Application.Features.State.Commands.UpdateState;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.MappingProfiles;

public class StateProfile : Profile
{
    public StateProfile()
    {
        CreateMap<StateDto, State>().ReverseMap();
        
        CreateMap<CreateStateCommand, State>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (int)src.Priority));

        CreateMap<UpdateStateCommand, State>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (int)src.Priority));
    }
}