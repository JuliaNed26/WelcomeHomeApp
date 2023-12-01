using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IEstablishmentService
    {
        Task<EstablishmentOutDTO> GetAsync(int id);

        IEnumerable<EstablishmentOutDTO> GetAll();

        IEnumerable<EstablishmentOutDTO> GetByEstablishmentType(int typeId);

        IEnumerable<EstablishmentOutDTO> GetByCity(Guid cityId);

        IEnumerable<EstablishmentOutDTO> GetByTypeAndCity(int typeId, Guid cityId);

        Task AddAsync(EstablishmentInDTO newEstablishment);

        Task UpdateAsync(EstablishmentOutDTO updatedEstablishment);

        Task DeleteAsync(int id);

        IEnumerable<EstablishmentTypeOutDTO> GetAllEstablishmentTypes();
    }
}
