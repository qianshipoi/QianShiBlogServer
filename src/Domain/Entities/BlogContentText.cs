namespace Domain.Entities;

public class BlogContentText : BaseEntity
{
    public string Text { get; set; } = default!;

    public string? HtmlText { get; set; }

    public virtual BlogContent Content { get; set; } = default!;
}

