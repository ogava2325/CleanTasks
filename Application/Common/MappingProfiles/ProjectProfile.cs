using Application.Common.Dtos;
using Application.Features.Project.Commands.CreateProject;
using Application.Features.Project.Commands.UpdateProject;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<CreateProjectCommand, Project>();
        
        CreateMap<UpdateProjectCommand, Project>();
        
        CreateMap<Project, ProjectDto>().ReverseMap();
    }
}