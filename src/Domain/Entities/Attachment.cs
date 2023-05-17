namespace Domain.Entities;

public class Attachment : BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public string Path { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public int Size { get; set; }
    public int Status { get; set; }
}