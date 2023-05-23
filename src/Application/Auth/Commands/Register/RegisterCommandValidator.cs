using FluentValidation;

namespace Application.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
	public RegisterCommandValidator()
	{
        RuleFor(v => v.Email)
            .EmailAddress()
            .MaximumLength(32)
            .MinimumLength(6)
            .NotEmpty();

        RuleFor(v => v.Password)
            .MaximumLength(32)
            .MinimumLength(6)
            .NotEmpty();
    }
}
