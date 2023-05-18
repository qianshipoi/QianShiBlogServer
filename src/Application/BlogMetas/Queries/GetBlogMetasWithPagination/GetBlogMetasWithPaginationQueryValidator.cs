using FluentValidation;

namespace Application.BlogMetas.Queries.GetBlogMetasWithPagination;

public class GetBlogMetasWithPaginationQueryValidator : AbstractValidator<GetBlogMetasWithPaginationQuery>
{
    public GetBlogMetasWithPaginationQueryValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Type in (1,2).")
            .NotEmpty().WithMessage("Type is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
