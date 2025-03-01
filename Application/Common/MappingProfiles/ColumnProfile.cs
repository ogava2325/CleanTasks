using Application.Common.Dtos;
using Application.Features.Column.Commands.CreateColumn;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class ColumnProfile : Profile
{
    public ColumnProfile()
    {
        CreateMap<Column, ColumnDto>().ReverseMap();
        
        CreateMap<CreateColumnCommand, Column>()
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
    }
}