using FluentValidation;

namespace Application.Features.Card.Commands.UpdateCard;

public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
{
    public UpdateCardCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        
        RuleFor(x => x.ColumnId)
            .NotEmpty().WithMessage("ColumnId is required.");
    }
}