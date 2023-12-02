using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface ISocialPayoutRepository
{
    IEnumerable<SocialPayout> GetAll();

    Task<SocialPayout?> GetByIdAsync(int id);

    Task AddWithStepsAsync(SocialPayout socialPayout, Dictionary<int, Step> steps);

    //Task UpdateWithStepsAsync(SocialPayout socialPayout, IEnumerable<int> stepIds);

    Task DeleteAsync(int id);

}
