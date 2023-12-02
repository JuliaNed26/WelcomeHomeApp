using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventTypeRepository
    {
        IEnumerable<EventType> GetAll();

        Task<EventType?> GetByIdAsync(int id);
        Task AddAsync(EventType newEventType);

        Task DeleteAsync(int id);

        Task UpdateAsync(EventType editedEventType);
    }
}
