using FluentValidation;

namespace Application.BlogContents.Commands.UpdateBlogContent;

public class UpdateBlogContentCommandValidator : AbstractValidator<UpdateBlogContentCommand>
{
    public UpdateBlogContentCommandValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0);

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
