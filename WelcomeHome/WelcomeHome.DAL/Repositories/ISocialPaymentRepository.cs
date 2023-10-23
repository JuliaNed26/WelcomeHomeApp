using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface ISocialPaymentRepository
    {
        Task<IEnumerable<SocialPayment>> GetAllAsync();

        Task<SocialPayment?> GetByIdAsync(Guid id);

        Task Add(SocialPayment newSocialPayment);

        Task Delete(Guid id);

        Task Update(SocialPayment editedSocialPayment);
    }
}
