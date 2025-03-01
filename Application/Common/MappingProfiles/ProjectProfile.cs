using Application.Common.Dtos;
using Application.Features.Project.Commands.CreateProject;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<CreateProjectCommand, Project>();
        
        CreateMap<Project, ProjectDto>().ReverseMap();
        
        CreateMap<Card, CardDto>().ReverseMap();
    }
}