using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Exceptions;
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
            var foundVacancy = await _context.Vacancies
	                                         .FindAsync(id)
	                                         .ConfigureAwait(false)
                               ?? throw new NotFoundException($"Vacancy with Id {id} not found for deletion.");
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
            _context.Vacancies.Attach(vacancy);
            _context.Entry(vacancy).State = EntityState.Unchanged;
        }
    }
}
