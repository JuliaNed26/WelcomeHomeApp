using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Task<Event?> GetByIdAsync(int id);

        IEnumerable<Event> GetByEventType(int eventTypeId);

        Task AddAsync(Event newEvent);

        Task DeleteAsync(int id);

        Task UpdateAsync(Event editedEvent);
    }
}

