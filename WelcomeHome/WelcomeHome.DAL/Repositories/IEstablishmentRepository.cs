using WelcomeHome.DAL.Dto;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEstablishmentRepository
    {
        IEnumerable<Establishment> GetAll(EstablishmentRetrievalFiltersDto? filters = null);

        Task<Establishment?> GetByIdAsync(long id);

        Task AddAsync(Establishment newEstablishment);

        Task DeleteAsync(long id);

        Task UpdateAsync(Establishment editedEstablishment);
    }
}
