using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IStepService
    {
        Task<StepOutDTO> GetAsync(int id);

        Task<IEnumerable<StepOutDTO>> GetAllAsync();

        Task<IEnumerable<StepOutDTO>> GetByEstablishmentTypeIdAsync(int establishmentTypeId);

        Task AddAsync(StepInDTO newStep);

        Task UpdateAsync(StepOutDTO stepWithUpdateInfo);

        Task DeleteAsync(int id);
    }
}
