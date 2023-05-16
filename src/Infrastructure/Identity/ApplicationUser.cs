using Application.Common.Models;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public List<RefreshToken>? RefreshTokens { get; set; }
    public bool OwnsToken(string token)
    {
        return this.RefreshTokens?.Find(x => x.Token == token) != null;
    }
}
