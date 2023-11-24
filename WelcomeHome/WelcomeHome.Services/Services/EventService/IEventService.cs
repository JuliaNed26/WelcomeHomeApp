using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IEventService
    {
        int GetCount();

        Task<EventOutDTO> GetAsync(Guid id);

        IEnumerable<EventOutDTO> GetAll();

        Task AddAsync(EventInDTO newEvent);

        Task UpdateAsync(EventOutDTO eventWithUpdateInfo);

        Task DeleteAsync(Guid id);
    }
}
