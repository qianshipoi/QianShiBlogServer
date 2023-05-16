namespace Application.Common.Models
{
    public class AuthenticationRequest
    {
        public AuthenticationRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
