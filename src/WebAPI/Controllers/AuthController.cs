using Application.Auth.Commands.Login;
using Application.Auth.Commands.Register;
using Application.Common.Models;
using Application.Common.Wrappers;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// 认证
/// </summary>
public class AuthController : ApiControllerBase
{
    [HttpPost("login")]
    public async Task<Response<AuthenticationResponse>> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("register")]
    public async Task Register(RegisterCommand command)
    {
        await Mediator.Send(command);
    }
}
