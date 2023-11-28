using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface ISocialPayoutService
    {
        int GetCount();

        Task<SocialPayoutOutDTO> GetAsync(Guid id);

        IEnumerable<SocialPayoutOutDTO> GetAll();

        Task AddAsync(SocialPayoutInDTO newPayout);

        Task UpdateAsync(SocialPayoutInDTO payoutWithUpdateInfo);

        Task DeleteAsync(Guid id);
    }
}
