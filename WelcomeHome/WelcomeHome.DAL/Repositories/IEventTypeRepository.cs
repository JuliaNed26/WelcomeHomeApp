using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventTypeRepository
    {
        IEnumerable<EventType> GetAll();

        Task<EventType?> GetByIdAsync(Guid id);
        Task AddAsync(EventType newEventType);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(EventType editedEventType);
    }
}
