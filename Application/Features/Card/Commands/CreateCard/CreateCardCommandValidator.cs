using FluentValidation;

namespace Application.Features.Card.Commands.CreateCard;

public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
{
    public CreateCardCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        
        RuleFor(x => x.ColumnId)
            .NotEmpty().WithMessage("ColumnId is required.");
        
    }
}