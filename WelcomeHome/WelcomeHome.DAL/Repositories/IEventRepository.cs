using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventRepository 
    {
        IEnumerable<Event> GetAll();

        Task<Event?> GetByIdAsync(Guid id);

        Task AddAsync(Event newEvent);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(Event editedEvent);
    }
}

