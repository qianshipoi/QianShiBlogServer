using FluentValidation;

namespace Application.BlogContents.Commands.CreateBlogContent;

public class CreateBlogContentCommandValidator : AbstractValidator<CreateBlogContentCommand>
{
    public CreateBlogContentCommandValidator()
    {
        RuleFor(p => p.Title)
            .MaximumLength(200)
            .NotEmpty()
            .NotNull();

        RuleFor(p => p.Subtitle)
            .MaximumLength(255);

        RuleFor(p => p.Order)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Status)
            .IsInEnum();

        RuleFor(p => p.Type)
            .IsInEnum();

        RuleFor(p => p.Text)
             .NotEmpty()
             .NotNull();
    }
}
