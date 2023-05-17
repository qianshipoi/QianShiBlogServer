namespace Domain.Entities;

public class UserInfo : BaseEntity
{
    public string Email { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public string Salt { get; set; } = default!;

    public DateTime Created { get; set; }

    public string? Avatar { get; set; }

    public string? NikeName { get; set; }
}

