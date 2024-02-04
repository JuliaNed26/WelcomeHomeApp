using WelcomeHome.DAL.Models;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IVolunteerService
    {
        Task<Volunteer?> RegisterVolunteerAsync(VolunteerRegisterDTO volunteer);

        IEnumerable<VolunteerOutDTO> GetAll();

        Task<VolunteerOutDTO> GetAsync(int id);

        Task AddVolunteerOrganizationAsync(EstablishmentVolunteerInDTO newEstablishment);
    }
}
