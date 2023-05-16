using FluentValidation;

namespace Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(v => v.UserName)
            .MaximumLength(32)
            .MinimumLength(3)
            .NotEmpty();

        RuleFor(v => v.Password)
            .MaximumLength(32)
            .MinimumLength(3)
            .NotEmpty();
    }
}


