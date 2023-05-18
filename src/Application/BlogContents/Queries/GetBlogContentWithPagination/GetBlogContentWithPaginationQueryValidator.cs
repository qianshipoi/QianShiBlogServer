using FluentValidation;

namespace Application.BlogContents.Queries.GetBlogContentWithPagination;

public class GetBlogContentWithPaginationQueryValidator:AbstractValidator<GetBlogContentWithPaginationQuery>
{
    public GetBlogContentWithPaginationQueryValidator()
    {
        RuleFor(x => x.Type)
          .IsInEnum()
          .NotEmpty().WithMessage("Type is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
