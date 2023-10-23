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
        Task<IEnumerable<Establishment>> GetAllAsync();

        Task<Establishment?> GetByIdAsync(Guid id);

        Task Add(Establishment newEstablishment);

        Task Delete(Guid id);

        Task Update(Establishment editedEstablishment);
    }
}
