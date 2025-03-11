using System.Windows.Input;
using Application.Common.Interfaces.Persistence.Repositories;
using Application.Features.Comment.Commands.DeleteComment;
using MediatR;

namespace Application.Features.Column.Commands.DeleteColumn;

public class DeleteColumnCommandHandler(
    IColumnRepository columnRepository) 
    : IRequestHandler<DeleteColumnCommand>
{
    public async Task Handle(DeleteColumnCommand request, CancellationToken cancellationToken)
    {
        var column = await columnRepository.GetByIdAsync(request.Id);
        
        ArgumentNullException.ThrowIfNull(column);
        
        await columnRepository.DeleteAsync(request.Id);
    }
}