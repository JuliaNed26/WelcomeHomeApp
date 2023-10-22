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
        Task<IEnumerable<EventType>> GetAllAsync();

        Task<EventType?> GetByIdAsync(Guid id);
        Task Add(EventType newEventType);

        Task Delete(Guid id);

        Task Update(Event editedEventType);
    }
}
