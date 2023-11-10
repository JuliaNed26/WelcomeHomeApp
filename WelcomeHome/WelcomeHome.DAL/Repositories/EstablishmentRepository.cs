﻿using WelcomeHome.DAL.Models;
using Microsoft.EntityFrameworkCore;

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
            await AttachEstablishmentTypeAsync(newEstablishment).ConfigureAwait(false);
            await AttachCityAsync(newEstablishment).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingEstablishment = await _context.Establishments.SingleAsync(e => e.Id == id).ConfigureAwait(false);
            _context.Establishments.Remove(existingEstablishment);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(Establishment editedEstablishment)
		{
			await AttachEstablishmentTypeAsync(editedEstablishment).ConfigureAwait(false);
			await AttachCityAsync(editedEstablishment).ConfigureAwait(false);
			_context.Establishments.Update(editedEstablishment);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task AttachEstablishmentTypeAsync(Establishment establishment)
        {
            var existingEstablishmentType = await _context.EstablishmentTypes.SingleAsync(et => et.Id == establishment.EstablishmentTypeId).ConfigureAwait(false);

            _context.EstablishmentTypes.Attach(existingEstablishmentType);
            _context.Entry(existingEstablishmentType).State = EntityState.Unchanged;
        }

        private async Task AttachCityAsync(Establishment establishment)
        {
            var existingCity = await _context.Cities.SingleAsync(c => c.Id == establishment.CityId).ConfigureAwait(false);

            _context.Cities.Attach(existingCity);
            _context.Entry(existingCity).State = EntityState.Unchanged;
        }
    }
}