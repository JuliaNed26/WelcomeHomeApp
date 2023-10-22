using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventsRepository 
    {
        Task<IEnumerable<Event>> GetAllAsync();

        Task<Event?> GetByIdAsync(Guid id);

        Task Add(Event newEvent);

        Task Delete(Guid id);

        Task Update(Event editedEvent);
    }
}

