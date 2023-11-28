using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface ISocialPayoutRepository
{
	IEnumerable<SocialPayout> GetAll();

	Task<SocialPayout?> GetByIdAsync(Guid id);

	Task AddWithStepsAsync(SocialPayout socialPayout, Dictionary<int, Step> steps);

	Task UpdateWithStepsAsync(SocialPayout socialPayout, Dictionary<int, Step> steps);

	Task DeleteAsync(Guid id);

}
