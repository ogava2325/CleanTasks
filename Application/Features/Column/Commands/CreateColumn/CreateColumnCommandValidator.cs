using FluentValidation;

namespace Application.Features.Column.Commands.CreateColumn;

public class CreateColumnCommandValidator : AbstractValidator<CreateColumnCommand>
{
    public CreateColumnCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.");
        
        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("ProjectId is required.");
    }
}