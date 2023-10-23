using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface ICountryRepository
{
	Task<Country?> GetByIdAsync(Guid id);

	IEnumerable<Country> GetAll();

	Task AddAsync(Country country);

	Task UpdateAsync(Country country);

	Task DeleteAsync(Guid id);
}
