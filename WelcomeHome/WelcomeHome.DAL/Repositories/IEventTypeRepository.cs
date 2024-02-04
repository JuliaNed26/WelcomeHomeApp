using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventTypeRepository
    {
        IEnumerable<EventType> GetAll();

        Task<EventType?> GetByIdAsync(int id);

        Task<EventType?> GetByNameAsync(string name);

        Task AddAsync(EventType newEventType);

        Task DeleteAsync(int id);

        Task UpdateAsync(EventType editedEventType);
    }
}
