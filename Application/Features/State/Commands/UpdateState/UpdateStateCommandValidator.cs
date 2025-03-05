using System.Reflection.Emit;
using FluentValidation;

namespace Application.Features.State.Commands.UpdateState;

public class UpdateStateCommandValidator : AbstractValidator<UpdateStateCommand>
{
    public UpdateStateCommandValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum();
        
        RuleFor(x => x.Priority)
            .IsInEnum();
        
        RuleFor(x => x.CardId)
            .NotEmpty().WithMessage("CardId is required.");
    }
}