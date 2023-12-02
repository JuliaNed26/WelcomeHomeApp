﻿using WelcomeHome.Services.DTO;
namespace WelcomeHome.Services.Services
{
    public interface ISocialPayoutService
    {
        int GetCount();

        Task<SocialPayoutOutDTO> GetAsync(int id);

        IEnumerable<SocialPayoutOutDTO> GetAll();

        Task AddAsync(SocialPayoutInDTO newPayout);

        Task UpdateAsync(SocialPayoutOutDTO payoutWithUpdateInfo);

        Task DeleteAsync(int id);
    }
}
