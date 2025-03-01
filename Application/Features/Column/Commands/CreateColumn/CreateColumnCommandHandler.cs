using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Column.Commands.CreateColumn;

public class CreateColumnCommandHandler(
    IMapper mapper,
    IColumnRepository columnRepository) 
    : IRequestHandler<CreateColumnCommand, Unit>
{
    public async Task<Unit> Handle(CreateColumnCommand request, CancellationToken cancellationToken)
    {
        var column = mapper.Map<Domain.Entities.Column>(request);
        
        column.CreatedBy = "some user";
        column.CreatedAtUtc = DateTimeOffset.UtcNow;

        await columnRepository.AddAsync(column);
        
        return Unit.Value;
    }
}