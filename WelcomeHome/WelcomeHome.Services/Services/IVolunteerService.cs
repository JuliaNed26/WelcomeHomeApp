using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IVolunteerService
    {
        int GetCount();

        Task<VolunteerOutDTO> GetAsync(Guid id);

        IEnumerable<VolunteerOutDTO> GetAll();

        Task AddAsync(VolunteerInDTO newVolunteer);

        Task UpdateAsync(VolunteerOutDTO volunteerWithUpdateInfo);

        Task DeleteAsync(Guid id);
    }
}
