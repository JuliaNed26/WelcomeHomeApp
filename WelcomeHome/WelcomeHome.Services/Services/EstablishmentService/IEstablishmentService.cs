using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services
{
    public interface IEstablishmentService
    {
        Task<EstablishmentOutDTO> GetAsync(Guid id);

        IEnumerable<EstablishmentOutDTO> GetAll();

        IEnumerable<EstablishmentOutDTO> GetByEstablishmentType(Guid typeId);

        IEnumerable<EstablishmentOutDTO> GetByCity(Guid cityId);

        IEnumerable<EstablishmentOutDTO> GetByTypeAndCity(Guid typeId, Guid cityId);

        Task AddAsync(EstablishmentInDTO newEstablishment);

        Task UpdateAsync(EstablishmentOutDTO updatedEstablishment);

        Task DeleteAsync(Guid id);

        IEnumerable<EstablishmentTypeOutDTO> GetAllEstablishmentTypes();
    }
}
