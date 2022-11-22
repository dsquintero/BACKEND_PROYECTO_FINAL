using API.Models.Entities;
using API.Models.Requests;

namespace API.Core.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<User> GetByUserCode(string username);
        Task<int> Register(UserRequest user);

    }
}
