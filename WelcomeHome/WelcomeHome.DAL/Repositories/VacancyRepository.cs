using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public sealed class VacancyRepository : IVacancyRepository
    {
        private readonly WelcomeHomeDbContext _context;
        
        public VacancyRepository(WelcomeHomeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Vacancy vacancy)
        {
            await _context.AddAsync(vacancy).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var foundVacancy = await _context.Vacancies.SingleAsync(v => v.Id == id).ConfigureAwait(false);
            _context.Vacancies.Remove(foundVacancy);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public IEnumerable<Vacancy> GetAll()
        {
            return _context.Vacancies
                .Include(v => v.Establishment)
                .AsNoTracking()
                .Select(v => v);
        }

        public async Task<Vacancy?> GetByIdAsync(Guid id)
        {
            return await _context.Vacancies
                .Include(v => v.Establishment)
                .AsNoTracking()
                .SingleOrDefaultAsync(v => v.Id == id)
                .ConfigureAwait(false);
        }

        public async Task UpdateAsync(Vacancy vacancy)
		{
			await AttachEstablishmentAsync(vacancy).ConfigureAwait(false);
			_context.Vacancies.Update(vacancy);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task AttachEstablishmentAsync(Vacancy vacancy)
        {
            var foundEstablishment = await _context.Establishments
                                         .SingleAsync(e => e.Id == vacancy.EstablishmentId)
                                         .ConfigureAwait(false);

            _context.Establishments.Attach(foundEstablishment);
            _context.Entry(foundEstablishment).State = EntityState.Unchanged;
        }
    }
}
