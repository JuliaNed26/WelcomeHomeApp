using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface ISocialPayoutRepository
{
	IEnumerable<SocialPayout> GetAll();

	Task<SocialPayout?> GetAsync(Guid id);

	Task AddWithStepsAsync(SocialPayout socialPayout, IEnumerable<Guid> stepIds);

	Task UpdateWithStepsAsync(SocialPayout socialPayout, IEnumerable<Guid> stepIds);

	Task DeleteAsync(Guid id);
}
