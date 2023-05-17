namespace Domain.Entities;

public class BlogMeta : BaseAuditableEntity
{
    public string Name { get; set; } = default!;

    public int Order { get; set; }

    public string? Description { get; set; }

    public string Type { get; set; } = default!;

    public int Count { get; set; }

    public int Parent { get; set; }
}
