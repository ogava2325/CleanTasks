using FluentValidation;

namespace Application.Features.User.Commands.AddUserToProject;

public class AddUserToProjectCommandValidator : AbstractValidator<AddUserToProjectCommand>
{
    public AddUserToProjectCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("Project ID is required.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.");
    }
}