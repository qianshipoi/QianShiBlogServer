namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? Modified { get; set; }
}
