using Application.Common.Mappings;

using Domain.Entities;
using Domain.Enums;

namespace Application.BlogContents.Queries.GetBlogContentWithPagination;

public class BlogContentDto : IMapFrom<BlogContent>
{
    public BlogContentDto()
    {
        Relationships = new HashSet<BlogRelationshipsDto>();
    }

    public string Title { get; set; } = default!;

    public string? Subtitle { get; set; }

    public int Order { get; set; }

    public BlogContentStatus Status { get; set; } = BlogContentStatus.Draft;

    public BlogContentType Type { get; set; }

    public bool AllowComment { get; set; }

    public int CommentCount { get; set; }

    public BlogContentText? Text { get; set; }

    public ICollection<BlogRelationshipsDto> Relationships { get; set; }
}
