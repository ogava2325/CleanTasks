using Application.Common.Dtos;
using Application.Features.Card.Commands.CreateCard;
using Application.Features.Card.Commands.UpdateCard;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class CardProfile : Profile
{
    public CardProfile()
    {
        CreateMap<Card, CardDto>().ReverseMap();
        
        CreateMap<UpdateCardCommand, Card>()
            .ForMember(dest => dest.ColumnId, opt => opt.MapFrom(src => src.ColumnId));
        
        CreateMap<CreateCardCommand, Card>()
            .ForMember(dest => dest.ColumnId, opt => opt.MapFrom(src => src.ColumnId));
    }
}