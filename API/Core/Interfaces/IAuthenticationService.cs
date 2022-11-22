using API.Models.Requests;
using API.Models.Responses;

namespace API.Core.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedResponse> Login(LoginRequest login);
        Task<AuthenticatedResponse> Refresh(RefreshRequest refreshRequest);
        Task Logout(string username);
        Task Register(UserRequest refreshRequest);

    }
}
