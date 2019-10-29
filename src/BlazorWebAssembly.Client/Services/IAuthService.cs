using BlazorAuthorization.Shared;
using System.Threading.Tasks;

namespace BlazorWebAssembly.Client.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}
