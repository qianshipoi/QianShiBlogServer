using FluentValidation;

namespace Application.BlogMetas.Commands.UpdateBlogMeta;

public class UpdateBlogMetaCommandValidator : AbstractValidator<UpdateBlogMetaCommand>
{
    public UpdateBlogMetaCommandValidator()
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
            .GreaterThanOrEqualTo(0)
            .Must((m,current) => m.Id != current)
            .WithMessage("'Parent' 不能为当前数据。");

        RuleFor(p => p.Order)
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.Id)
            .GreaterThan(0);
    }
}
