using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;

using Domain.Helpers;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace Application.Auth.Commands.Login;

public class LoginCommand : IRequest<Response<AuthenticationResponse>>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<AuthenticationResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(IApplicationDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<Response<AuthenticationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.UserInfos
            .AsNoTracking()
            .Where(x => x.Email == request.UserName.Trim())
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            return new Response<AuthenticationResponse>("用户不存在");
        }

        var result = PasswordHasher.CompareHashPassword(request.Password, user.Salt, user.PasswordHash);

        if (!result)
        {
            return new Response<AuthenticationResponse>("密码错误");
        }

        return new Response<AuthenticationResponse>(new AuthenticationResponse
        {
            Id = user.Id.ToString(),
            Email = user.Email,
            UserName = user.NikeName,
            JWToken = _jwtService.GetJwtToken(new List<Claim>()),
            RefreshToken = _jwtService.GenerateRefreshToken("").Token
        });
    }
}


