using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEstablishmentRepository
    {
        IEnumerable<Establishment> GetAll();

        Task<Establishment?> GetByIdAsync(Guid id);

        Task AddAsync(Establishment newEstablishment);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(Establishment editedEstablishment);
    }
}
