using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IStepRepository
{
	IEnumerable<Step> GetAll();

	Task<Step?> GetAsync(Guid id);

	Task AddAsync(Step step);

	Task UpdateAsync(Step step);

	Task DeleteAsync(Guid id);
}
