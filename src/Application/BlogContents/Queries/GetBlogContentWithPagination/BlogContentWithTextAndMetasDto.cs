using Application.BlogMetas.Queries.GetBlogMetasWithPagination;
using Application.Common.Mappings;

using Domain.Entities;
using Domain.Enums;

namespace Application.BlogContents.Queries.GetBlogContentWithPagination;

public class BlogContentWithTextAndMetasDto : IMapFrom<BlogContent>
{
    public BlogContentWithTextAndMetasDto()
    {
        Metas = new List<BlogMetaDto>();
    }

    public int Id { get; set; }
    public string Title { get; set; } = default!;

    public string? Subtitle { get; set; }

    public int Order { get; set; }

    public BlogContentStatus Status { get; set; } = BlogContentStatus.Draft;

    public BlogContentType Type { get; set; }

    public bool AllowComment { get; set; }

    public int CommentCount { get; set; }

    public BlogContentTextDto? Text { get; set; }

    public List<BlogMetaDto> Metas { get; set; }
}

