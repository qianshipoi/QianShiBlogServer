using Application.Common.Interfaces;

using Domain.Helpers;

using MediatR;

namespace Application.Auth.Commands.Register;

public class RegisterCommand : IRequest
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IApplicationDbContext _context;

    public RegisterCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (passwordHashed, salt) = PasswordHasher.HashPassword(request.Password);

        _context.UserInfos.Add(new Domain.Entities.UserInfo
        {
            Created = DateTime.UtcNow,
            Email = request.Email,
            NikeName = request.Email,
            PasswordHash = passwordHashed,
            Salt = salt
        });

        await _context.SaveChangesAsync(cancellationToken);
    }
}
