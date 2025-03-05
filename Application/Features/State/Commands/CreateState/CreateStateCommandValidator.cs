using FluentValidation;

namespace Application.Features.State.Commands.CreateState;

public class CreateStateCommandValidator : AbstractValidator<CreateStateCommand>
{
    public CreateStateCommandValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum();
        
        RuleFor(x => x.Priority)
            .IsInEnum();
        
        RuleFor(x => x.CardId)
            .NotEmpty().WithMessage("CardId is required.");
    }
}