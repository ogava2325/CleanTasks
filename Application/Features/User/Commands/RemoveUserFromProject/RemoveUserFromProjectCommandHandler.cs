using Application.Common.Abstraction;
using Application.Common.Interfaces.Persistence.Repositories;
using MediatR;

namespace Application.Features.User.Commands.RemoveUserFromProject;

public class RemoveUserFromProjectCommandHandler(
    IProjectRepository projectRepository,
    IRoleRepository roleRepository) 
    : IRequestHandler<RemoveUserFromProjectCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RemoveUserFromProjectCommand request, CancellationToken cancellationToken)
    {
        var existingProjectMember = await projectRepository.IsUserInProjectAsync(request.ProjectId, request.UserId);
        
        if (!existingProjectMember)
        {
            return Result<string>.Failure("User is not a member of the project.");
        }

        if (request.CurrentUserId == request.UserId)
        {
            return Result<string>.Failure("You can't remove yourself from the project.");
        }
        
        await projectRepository.RemoveUserFromProjectAsync(request.ProjectId, request.UserId);
        return Result<string>.Success("User removed from project successfully.");
    }
}