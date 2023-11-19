using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IVacancyRepository
    {
        Task<Vacancy?> GetByIdAsync(Guid id);

        IEnumerable<Vacancy> GetAll();

        Task AddAsync(Vacancy vacancy);

        Task UpdateAsync(Vacancy vacancy);

        Task DeleteAsync(Guid id);
    }
}
