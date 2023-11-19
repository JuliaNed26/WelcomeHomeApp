using WelcomeHome.DAL.Models;
using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Exceptions;

namespace WelcomeHome.DAL.Repositories
{
    public class EstablishmentTypeRepository : IEstablishmentTypeRepository
	{
		private readonly WelcomeHomeDbContext _context;

		public EstablishmentTypeRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<EstablishmentType> GetAll()
        {
            return _context.EstablishmentTypes.Include(et => et.Establishments)
                                              .AsNoTracking()
                                              .Select(et => et);
        }

        public async Task<EstablishmentType?> GetByIdAsync(Guid id)
        {
            return await _context.EstablishmentTypes.Include(et => et.Establishments)
                                                    .FirstOrDefaultAsync(e => e.Id == id)
                                                    .ConfigureAwait(false);
        }

        public async Task AddAsync(EstablishmentType newEstablishmentType)
        {

            await _context.EstablishmentTypes.AddAsync(newEstablishmentType).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingEstablishmentType = await _context.EstablishmentTypes
	                                                      .FindAsync(id)
	                                                      .ConfigureAwait(false)
				                            ?? throw new NotFoundException($"Establishment with Id {id} not found for deletion.");
            _context.EstablishmentTypes.Remove(existingEstablishmentType);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(EstablishmentType editedEstablishmentType)
        {
            _context.EstablishmentTypes.Update(editedEstablishmentType);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
