using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IEventService
    {
        int GetCount();

        Task<EventOutDTO> GetAsync(Guid id);

        IEnumerable<EventOutDTO> GetAll();

        Task AddAsync(EventInDTO newEvent);

        Task UpdateAsync(EventInDTO eventWithUpdateInfo);

        Task DeleteAsync(Guid id);
    }
}
