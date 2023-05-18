namespace Domain.Entities;

public class BlogContent : BaseAuditableEntity
{
    public string Title { get; set; } = default!;

    public string? Subtitle { get; set; }

    public int Order { get; set; }

    public BlogContentStatus Status { get; set; } = BlogContentStatus.Draft;

    public BlogContentType Type { get; set; }

    public bool AllowComment { get; set; }

    public int CommentCount { get; set; }

    public virtual BlogContentText Text { get; set; } = default!;

    public List<BlogMeta> Metas { get; set; } = new();
}

