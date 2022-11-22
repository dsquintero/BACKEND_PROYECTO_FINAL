using API.Models.Entities;

namespace API.Core.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByToken(string token);
        Task<int> Create(RefreshToken refreshToken);
        Task<int> Delete(string username);
    }
}
