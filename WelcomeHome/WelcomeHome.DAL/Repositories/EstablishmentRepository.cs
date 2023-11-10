using WelcomeHome.DAL.Models;
using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Exceptions;

namespace WelcomeHome.DAL.Repositories
{
    public class EstablishmentRepository : IEstablishmentRepository
    {
        private WelcomeHomeDbContext _context;

        public EstablishmentRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Establishment> GetAll()
        {
            return _context.Establishments.Include(e => e.Events)
                                          .Include(e => e.City)
                                          .Include(e => e.EstablishmentType)
                                          .AsNoTracking()
                                          .Select(e => e);
        }

        public async Task<Establishment?> GetByIdAsync(Guid id)
        {
            return await _context.Establishments.Include(e => e.Events)
                                                .Include(e => e.City)
                                                .Include(e => e.EstablishmentType)
                                                .FirstOrDefaultAsync(e => e.Id == id)
                                                .ConfigureAwait(false);
        }

        public async Task AddAsync(Establishment newEstablishment)
        {

            await _context.Establishments.AddAsync(newEstablishment).ConfigureAwait(false);
            await AttachEstablishmentTypeAsync(newEstablishment.EstablishmentType).ConfigureAwait(false);
            await AttachCityAsync(newEstablishment.City).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
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
			await AttachEstablishmentTypeAsync(editedEstablishment.EstablishmentType).ConfigureAwait(false);
			await AttachCityAsync(editedEstablishment.City).ConfigureAwait(false);
			_context.Establishments.Update(editedEstablishment);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task AttachEstablishmentTypeAsync(EstablishmentType establishmentType)
        {
            _context.EstablishmentTypes.Attach(establishmentType);
            _context.Entry(establishmentType).State = EntityState.Unchanged;
        }

        private async Task AttachCityAsync(City city)
        {
            _context.Cities.Attach(city);
            _context.Entry(city).State = EntityState.Unchanged;
        }
    }
}