namespace IdentityWebAPI.Services
{
    public interface IAuthService
    {
        Task<(int status, string message)> RegisterAsync(RegisterModel model);
        Task<(int status, string token)> LoginAsync(LoginModel model);
    }
}
