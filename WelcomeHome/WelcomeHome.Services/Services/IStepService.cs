using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IStepService
    {
        Task<StepOutDTO> GetAsync(Guid id);

        IEnumerable<StepOutDTO> GetAll();

        IEnumerable<StepOutDTO> GetBySocialPayout(Guid payoutId);

        Task AddAsync(StepInDTO newStep);

        Task UpdateAsync(StepOutDTO updatedStep);

        Task DeleteAsync(Guid id);
    }
}