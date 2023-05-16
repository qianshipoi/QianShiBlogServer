using Application.Common.Interfaces;
using Application.Common.Models;

using Domain.Settings;

using Infrastructure.Common;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JWTSettings _jwtSettings;

    public JwtService(IOptions<JWTSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public RefreshToken GenerateRefreshToken(string ipAddress)
    {
        return new RefreshToken
        {
            Token = RandomTokenString(),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };
    }
    private string RandomTokenString()
    {
        using var rngCryptoServiceProvider = RandomNumberGenerator.Create();
        var randomBytes = new byte[40];
        rngCryptoServiceProvider.GetBytes(randomBytes);
        // convert random bytes to hex string
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }

    public string GetJwtToken(IEnumerable<Claim> claim)
    {
        var roleClaims = new List<Claim>();

        //for (int i = 0; i < roles.Count; i++)
        //{
        //    roleClaims.Add(new Claim("roles", roles[i]));
        //}

        string ipAddress = IpHelper.GetIpAddress();

        var claims = new[]
        {
                //new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                //new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
        //.Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(_jwtSettings.Duration),
            signingCredentials: signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
