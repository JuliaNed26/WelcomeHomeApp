using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteAsync(long id)
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
                .Include(v => v.City)
                .AsNoTracking()
                .Select(v => v);
        }

        public async Task<Vacancy?> GetByIdAsync(long id)
        {
            return await _context.Vacancies
                .Include(v => v.City)
                .AsNoTracking()
                .SingleOrDefaultAsync(v => v.Id == id)
                .ConfigureAwait(false);
        }

        public async Task UpdateAsync(Vacancy vacancy)
        {
            if (vacancy.Id == 0)
            {
                throw new NotFoundException($"Vacancy with id {vacancy.Id} was not found");
            }
            AttachEstablishment(vacancy);
            _context.Vacancies.Update(vacancy);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private void AttachEstablishment(Vacancy vacancy)
        {
            _context.Vacancies.Attach(vacancy);
            _context.Entry(vacancy).State = EntityState.Unchanged;
        }
    }
}
