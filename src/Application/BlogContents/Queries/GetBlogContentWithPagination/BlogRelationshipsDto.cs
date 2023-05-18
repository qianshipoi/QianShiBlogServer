using Application.Common.Mappings;

using Domain.Entities;

namespace Application.BlogContents.Queries.GetBlogContentWithPagination;

public class BlogRelationshipsDto : IMapFrom<BlogRelationships>
{
    public BlogMetaDto Meta { get; set; } = default!;
}
