using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Dto;
using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public class EstablishmentRepository : IEstablishmentRepository
    {
        private readonly WelcomeHomeDbContext _context;

        public EstablishmentRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Establishment> GetAll(EstablishmentRetrievalFiltersDto? filters = null)
        {
            var allEstablishments = _context.Establishments.Include(e => e.Events)
                                                           .Include(e => e.City)
                                                           .Include(e => e.EstablishmentType)
                                                           .AsQueryable();

            if (filters != null)
            {
                allEstablishments = allEstablishments
                                    .Where(e => filters.EstablishmentTypeId == null || e.EstablishmentTypeId == filters.EstablishmentTypeId)
                                    .Where(e => filters.CityId == null || e.CityId == filters.CityId);
            }

            return allEstablishments;
        }

        public async Task<Establishment?> GetByIdAsync(long id)
        {
            return await _context.Establishments.Include(e => e.Events)
                                                .Include(e => e.City)
                                                .Include(e => e.EstablishmentType)
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(e => e.Id == id)
                                                .ConfigureAwait(false);
        }

        public async Task AddAsync(Establishment newEstablishment)
        {
            await AttachEstablishmentTypeAsync(newEstablishment.EstablishmentTypeId).ConfigureAwait(false);
            await AttachCityAsync(newEstablishment.CityId).ConfigureAwait(false);
            await _context.Establishments.AddAsync(newEstablishment).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(long id)
        {
            var existingEstablishment = await _context.Establishments
                                              .FindAsync(id)
                                              .ConfigureAwait(false)
                                        ?? throw new NotFoundException($"Establishment with Id {id} not found for deletion.");
            _context.Establishments.Remove(existingEstablishment);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(Establishment editedEstablishment)
        {
            await AttachEstablishmentTypeAsync(editedEstablishment.EstablishmentTypeId).ConfigureAwait(false);
            await AttachCityAsync(editedEstablishment.CityId).ConfigureAwait(false);

            if (editedEstablishment.Id == 0)
            {
                throw new NotFoundException($"Establishment with id {editedEstablishment.Id} was not found");
            }
            _context.Establishments.Update(editedEstablishment);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task AttachEstablishmentTypeAsync(long establishmentTypeId)
        {
            var foundType = await _context.EstablishmentTypes
                                          .FirstOrDefaultAsync(et => et.Id == establishmentTypeId)
                                          .ConfigureAwait(false)
                            ?? throw new NotFoundException($"Establishment type with id {establishmentTypeId} was not found");

            _context.EstablishmentTypes.Attach(foundType);
            _context.Entry(foundType).State = EntityState.Unchanged;
        }

        private async Task AttachCityAsync(long cityId)
        {
            var foundCity = await _context.Cities
                                .FirstOrDefaultAsync(c => c.Id == cityId)
                                .ConfigureAwait(false)
                            ?? throw new NotFoundException($"City with id {cityId} was not found");

            _context.Cities.Attach(foundCity);
            _context.Entry(foundCity).State = EntityState.Unchanged;
        }
    }
}