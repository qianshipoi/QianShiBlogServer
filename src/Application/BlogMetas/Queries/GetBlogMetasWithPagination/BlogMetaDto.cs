using Application.Common.Mappings;

using Domain.Entities;
using Domain.Enums;

namespace Application.BlogMetas.Queries.GetBlogMetasWithPagination;

public class BlogMetaDto : IMapFrom<BlogMeta>
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public int Order { get; set; }

    public string? Description { get; set; }

    public BlogMetaType Type { get; set; }

    public int Count { get; set; }

    public int Parent { get; set; }
}
