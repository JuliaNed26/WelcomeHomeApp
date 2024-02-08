using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface ICityRepository
{
    Task<City?> GetByIdAsync(long id);

    IEnumerable<City> GetAll();

    Task AddAsync(City city);

    Task UpdateAsync(City city);

    Task DeleteAsync(long id);
}
