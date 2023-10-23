using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface ICityRepository
{
	Task<City?> GetByIdAsync(int id);

	IEnumerable<City> GetAll();

	Task AddAsync(City city);

	Task UpdateAsync(City city);

	Task DeleteAsync(int id);
}
