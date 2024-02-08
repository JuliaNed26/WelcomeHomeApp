using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Task<Event?> GetByIdAsync(long id);

        IEnumerable<Event> GetByEventType(long eventTypeId);

        Task AddAsync(Event newEvent);

        Task DeleteAsync(long id);

        Task UpdateAsync(Event editedEvent);
    }
}

