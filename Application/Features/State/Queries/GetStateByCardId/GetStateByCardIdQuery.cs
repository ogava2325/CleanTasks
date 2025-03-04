using Application.Common.Dtos;
using MediatR;

namespace Application.Features.State.Queries.GetStateByCardId;

public record GetStateByCardIdQuery(Guid CardId) : IRequest<StateDto>;