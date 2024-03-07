using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventTypeRepository
    {
        IEnumerable<EventType> GetAll();

        Task<EventType?> GetByIdAsync(long id);

        Task<EventType?> GetByNameAsync(string name);

        Task AddAsync(EventType newEventType);

        Task DeleteAsync(long id);

        Task UpdateAsync(EventType editedEventType);
    }
}
