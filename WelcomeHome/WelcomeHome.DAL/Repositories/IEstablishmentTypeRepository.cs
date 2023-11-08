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

        Task<EstablishmentType?> GetByIdAsync(Guid id);
        Task Add(EstablishmentType newEstablishmentType);

        Task Delete(Guid id);

        Task Update(EstablishmentType editedEstablishmentType);
    }
}
