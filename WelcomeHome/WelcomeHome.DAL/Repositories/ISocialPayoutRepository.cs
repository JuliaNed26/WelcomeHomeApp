using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface ISocialPayoutRepository
{
    IEnumerable<SocialPayout> GetAll();

    Task<SocialPayout?> GetByIdAsync(int id);

    Task AddAsync(SocialPayout socialPayout);

    Task UpdateAsync(SocialPayout socialPayout);

    Task DeleteAsync(int id);

}
