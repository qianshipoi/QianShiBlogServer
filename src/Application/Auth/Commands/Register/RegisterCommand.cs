using Application.Common.Interfaces;

using MediatR;

namespace Application.Auth.Commands.Register;

public class RegisterCommand : IRequest
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IAccountService _accountService;

    public RegisterCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _accountService.RegisterAsync(new Common.Models.RegisterRequest(request.Email, request.Password),"");
    }
}
