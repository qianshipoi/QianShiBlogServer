using Application.Common.Mappings;

using Domain.Entities;

namespace Application.BlogContents.Queries.GetBlogContentWithPagination;

public class BlogContentTextDto : IMapFrom<BlogContentText>
{
    public string Text { get; set; } = default!;
    public string? HtmlText { get; set; }
}

