using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Dto;
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

        public IEnumerable<VacancyWithTotalPagesCount> GetAll(PaginationOptionsDto paginationOptions)
        {
            var vacancies = _context.VacanciesWithTotalPagesCounts
                                                                    .FromSqlRaw($"EXEC GetVacancyPageWithTotalVacanciesCount @page = {paginationOptions.PageNumber}," +
                                                                                $"@countOnPage = {paginationOptions.CountOnPage}");
            return vacancies;
        }

        public async Task<Vacancy?> GetByIdAsync(long id)
        {
            return await _context.Vacancies
                .Include(v => v.City)
                .AsNoTracking()
                .SingleOrDefaultAsync(v => v.Id == id)
                .ConfigureAwait(false);
        }

        public async Task AddAsync(Vacancy vacancy)
        {
            await AttachCityAsync(vacancy.CityId).ConfigureAwait(false);
            await _context.AddAsync(vacancy).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(Vacancy vacancy)
        {
            if (vacancy.Id == 0)
            {
                throw new NotFoundException($"Vacancy with id {vacancy.Id} was not found");
            }
            await AttachCityAsync(vacancy.CityId).ConfigureAwait(false);
            _context.Vacancies.Update(vacancy);

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

        private async Task AttachCityAsync(long cityId)
        {
            var foundCity = await _context.Cities.Where(c => c.Id == cityId)
                                                 .SingleOrDefaultAsync()
                            ?? throw new NotFoundException($"City with id {cityId} was not found");

            _context.Cities.Attach(foundCity);
            _context.Entry(foundCity).State = EntityState.Unchanged;
        }
    }
}
