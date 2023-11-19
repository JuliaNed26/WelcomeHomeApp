using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface ISocialPayoutService
    {
        int GetCount();

        Task<SocialPayoutOutDTO> GetAsync(Guid id);

        IEnumerable<SocialPayoutOutDTO> GetAll();

        Task AddAsync(SocialPayoutInDTO newPayout);

        Task UpdateAsync(SocialPayoutOutDTO payoutWithUpdateInfo);

        Task DeleteAsync(Guid id);
    }
}
