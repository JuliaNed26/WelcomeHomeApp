﻿using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface ISocialPayoutService
    {
        int GetCount();

        Task<SocialPayoutOutDTO> GetAsync(long id);

        IEnumerable<SocialPayoutListItemDTO> GetAll();

        Task AddAsync(SocialPayoutInDTO newPayout);

        //Task UpdateAsync(SocialPayoutInDTO payoutWithUpdateInfo);

        Task DeleteAsync(long id);
    }
}