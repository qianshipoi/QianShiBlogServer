namespace Domain.Entities;

public class BlogRelationships : BaseEntity
{
    public int Cid { get; set; }

    public int Mid { get; set; }

    public virtual BlogMeta? Meta { get; set; }

    public virtual BlogContent? Content { get; set; }
}