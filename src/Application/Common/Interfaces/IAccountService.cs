using Application.Common.Models;
using Application.Common.Wrappers;

namespace Application.Common.Interfaces;

public interface IAccountService
{
    Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);

    Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
}
