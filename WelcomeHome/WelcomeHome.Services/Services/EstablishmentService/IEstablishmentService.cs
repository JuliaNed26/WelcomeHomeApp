using WelcomeHome.Services.DTO;
using WelcomeHome.Services.DTO.EstablishmentDTO;

namespace WelcomeHome.Services.Services.EstablishmentService
{
    public interface IEstablishmentService
    {
        Task<EstablishmentOutDTO> GetAsync(int id);

        IEnumerable<EstablishmentOutDTO> GetAll(EstablishmentFiltersDto filters);

        Task AddAsync(EstablishmentInDTO newEstablishment);

        Task UpdateAsync(EstablishmentOutDTO updatedEstablishment);

        Task DeleteAsync(int id);

        IEnumerable<EstablishmentTypeOutDTO> GetAllEstablishmentTypes();
    }
}
