using WelcomeHome.Services.DTO.EventDto;

namespace WelcomeHome.Services.Services.EventService
{
    public interface IEventService
    {
        Task<EventFullInfoDTO> GetAsync(int id);

        IEnumerable<EventFullInfoDTO> GetAll();

        Task<IEnumerable<EventFullInfoDTO>> GetPsychologicalServicesAsync();

        Task AddAsync(EventInDTO newEvent);

        Task AddPsychologicalServiceAsync(EventInDTO newEvent);

        Task UpdateAsync(EventFullInfoDTO eventWithUpdateInfo);

        Task DeleteAsync(int id);
    }
}
