﻿using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IStepService
    {
        Task<StepOutDTO> GetAsync(Guid id);

        Task<IEnumerable<StepOutDTO>> GetAllAsync();

        Task<IEnumerable<StepOutDTO>> GetByEstablishmentTypeIdAsync(Guid establishmentTypeId);

        Task AddAsync(StepInDTO newStep);

        Task UpdateAsync(StepOutDTO stepWithUpdateInfo);

        Task DeleteAsync(Guid id);
    }
}