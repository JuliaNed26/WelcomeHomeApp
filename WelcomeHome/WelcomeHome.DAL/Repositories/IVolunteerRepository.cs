using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IVolunteerRepository
{
	Task<Volunteer?> GetByIdAsync(Guid id);

	IEnumerable<Volunteer> GetAll();

	Task AddAsync(Volunteer volunteer);

	Task UpdateAsync(Volunteer volunteer);

	Task DeleteAsync(Guid id);
}
