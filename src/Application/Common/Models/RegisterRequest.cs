namespace Application.Common.Models;

public class RegisterRequest
{
    public RegisterRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}
