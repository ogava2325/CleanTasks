using Application.Common.Dtos;
using Application.Common.Interfaces.Persistence.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Column.Queries;

public class GetColumnsByProjectIdQueryHandler(
    IMapper mapper,
    IColumnRepository columnRepository) 
    : IRequestHandler<GetColumnsByProjectIdQuery, IEnumerable<ColumnDto>>
{
    public async Task<IEnumerable<ColumnDto>> Handle(GetColumnsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        var columns = await columnRepository.GetColumnsByProjectIdAsync(request.ProjectId);
        
        return mapper.Map<IEnumerable<ColumnDto>>(columns);
    }
}