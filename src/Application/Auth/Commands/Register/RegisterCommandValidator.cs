using FluentValidation;

namespace Application.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
	public RegisterCommandValidator()
	{
        RuleFor(v => v.Email)
            .MaximumLength(32)
            .MinimumLength(3)
            .NotEmpty();

        RuleFor(v => v.Password)
            .MaximumLength(32)
            .MinimumLength(3)
            .NotEmpty();
    }
}
