using WelcomeHome.Services.DTO;
using WelcomeHome.Services.DTO.EstablishmentDTO;

namespace WelcomeHome.Services.Services.EstablishmentService
{
    public interface IEstablishmentService
    {
        Task<EstablishmentFullInfoDTO> GetAsync(int id);

        IEnumerable<EstablishmentFullInfoDTO> GetAll(EstablishmentFiltersDto filters);

        Task AddAsync(EstablishmentInDTO newEstablishment);

        Task UpdateAsync(EstablishmentFullInfoDTO updatedEstablishment);

        Task DeleteAsync(int id);

        IEnumerable<EstablishmentTypeOutDTO> GetAllEstablishmentTypes();
    }
}
