using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEstablishmentTypeRepository
    {
        IEnumerable<EstablishmentType> GetAll();

        Task<EstablishmentType?> GetByIdAsync(long id);
        Task AddAsync(EstablishmentType newEstablishmentType);

        Task DeleteAsync(long id);

        Task UpdateAsync(EstablishmentType editedEstablishmentType);
    }
}
