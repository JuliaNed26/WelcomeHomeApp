using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEstablishmentTypeRepository
    {
        IEnumerable<EstablishmentType> GetAll();

        Task<EstablishmentType?> GetByIdAsync(int id);
        Task AddAsync(EstablishmentType newEstablishmentType);

        Task DeleteAsync(int id);

        Task UpdateAsync(EstablishmentType editedEstablishmentType);
    }
}
