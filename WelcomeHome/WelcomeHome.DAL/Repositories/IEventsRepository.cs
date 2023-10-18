using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventsRepository 
    {
        Task<IEnumerable<Event>> GetEventsAsync();

        Task<Event?> GetEventByIdAsync(int id);

        Task AddEvent(Event newEvent);

        Task DeleteEvent(int id);

        Task UpdateEvent(int id, Event editedEvent);
    }
}

