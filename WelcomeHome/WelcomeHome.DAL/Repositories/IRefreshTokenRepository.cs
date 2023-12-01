using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);

    Task AddAsync(RefreshToken refreshToken);

    Task UpdateAsync(RefreshToken updatedRefreshToken);

    Task DeleteForUserAsync(Guid id);

    Task DeleteAllForUserAsync(Guid userId);
}
