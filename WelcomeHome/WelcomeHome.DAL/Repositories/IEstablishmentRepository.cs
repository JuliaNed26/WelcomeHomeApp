using WelcomeHome.DAL.Dto;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEstablishmentRepository
    {
        IEnumerable<Establishment> GetAll(EstablishmentRetrievalFiltersDto? filters = null);

        Task<Establishment?> GetByIdAsync(int id);

        Task AddAsync(Establishment newEstablishment);

        Task DeleteAsync(int id);

        Task UpdateAsync(Establishment editedEstablishment);
    }
}
