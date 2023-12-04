using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IVacancyRepository
    {
        Task<Vacancy?> GetByIdAsync(int id);

        IEnumerable<Vacancy> GetAll();

        Task AddAsync(Vacancy vacancy);

        Task UpdateAsync(Vacancy vacancy);

        Task DeleteAsync(int id);
    }
}
