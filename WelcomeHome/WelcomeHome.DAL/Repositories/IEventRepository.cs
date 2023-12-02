using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Task<Event?> GetByIdAsync(int id);

        Task AddAsync(Event newEvent);

        Task DeleteAsync(int id);

        Task UpdateAsync(Event editedEvent);
    }
}

