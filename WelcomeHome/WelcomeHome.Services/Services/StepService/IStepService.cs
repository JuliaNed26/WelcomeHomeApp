using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IStepService
    {
        Task<StepOutDTO> GetAsync(long id);

        Task<IEnumerable<StepOutDTO>> GetAllAsync();

        Task<IEnumerable<StepOutDTO>> GetByEstablishmentTypeIdAsync(long establishmentTypeId);

        Task AddAsync(StepInDTO newStep);

        Task UpdateAsync(StepOutDTO stepWithUpdateInfo);

        Task DeleteAsync(long id);
    }
}
