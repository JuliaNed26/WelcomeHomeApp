using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories;

public interface IVolunteerRepository
{
    Task<Volunteer?> GetByIdAsync(int id);

    IEnumerable<Volunteer> GetAll();

    Task<Volunteer?> AddAsync(int id, Volunteer volunteer);

    Task UpdateAsync(Volunteer volunteer);

    Task DeleteAsync(int id);
}
