using WelcomeHome.Services.DTO;
using WelcomeHome.Services.DTO.EstablishmentDTO;

namespace WelcomeHome.Services.Services
{
    public interface IEstablishmentService
    {
        Task<EstablishmentOutDTO> GetAsync(int id);

        IEnumerable<EstablishmentOutDTO> GetAll(EstablishmentFiltersDto filters);

        Task AddAsync(EstablishmentInDTO newEstablishment);

        Task AddVolunteerAsync(EstablishmentVolunteerInDTO newEstablishment);

        Task UpdateAsync(EstablishmentOutDTO updatedEstablishment);

        Task DeleteAsync(int id);

        IEnumerable<EstablishmentTypeOutDTO> GetAllEstablishmentTypes();
    }
}
