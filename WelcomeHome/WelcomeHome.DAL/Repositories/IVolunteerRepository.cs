using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IVolunteerRepository
{
    Task<Volunteer?> GetByIdAsync(long id);

    IEnumerable<Volunteer> GetAll();

    Task<Volunteer?> AddAsync(long id, Volunteer volunteer);

    Task UpdateAsync(Volunteer volunteer);

    Task DeleteAsync(long id);
}
