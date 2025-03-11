using Application.Common.Dtos;
using Application.Features.Comment.Commands.CreateComment;
using Application.Features.Comment.Commands.UpdateComment;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
        
        CreateMap<CreateCommentCommand, Comment>();
        
        CreateMap<UpdateCommentCommand, Comment>();
    }
}