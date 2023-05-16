using Application.Common.Models;

using System.Net;
using System.Security.Claims;

namespace Application.Common.Interfaces;

public interface IJwtService
{
    string GetJwtToken(IEnumerable<Claim> claim);
    RefreshToken GenerateRefreshToken(string ipAddress);
}
