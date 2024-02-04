using WelcomeHome.Services.DTO.EventDto;

namespace WelcomeHome.Services.Services.EventService
{
    public interface IEventService
    {
        Task<EventOutDTO> GetAsync(int id);

        IEnumerable<EventOutDTO> GetAll();

        Task<IEnumerable<EventOutDTO>> GetPsychologicalServicesAsync();

        Task AddAsync(EventInDTO newEvent);

        Task AddPsychologicalServiceAsync(EventInDTO newEvent);

        Task UpdateAsync(EventOutDTO eventWithUpdateInfo);

        Task DeleteAsync(int id);
    }
}
