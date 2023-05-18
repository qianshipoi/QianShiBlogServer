using FluentValidation;

namespace Application.BlogMetas.Commands.CreateBlogMeta;

public class CreateBlogMetaCommandValidator : AbstractValidator<CreateBlogMetaCommand>
{
    public CreateBlogMetaCommandValidator()
    {
        RuleFor(p => p.Name)
            .MaximumLength(32)
            .NotEmpty()
            .NotNull();

        RuleFor(p => p.Description)
            .MaximumLength(200);

        RuleFor(p => p.Type)
            .NotNull()
            .IsInEnum();

        RuleFor(p => p.Parent)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Order)
            .GreaterThanOrEqualTo(0);
    }
}

