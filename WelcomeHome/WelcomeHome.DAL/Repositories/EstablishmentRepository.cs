﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Establishment?> GetByIdAsync(int id)
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
            await _context.Establishments.AddAsync(newEstablishment).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
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
            AttachEstablishmentType(editedEstablishment);
            AttachCity(editedEstablishment);
            _context.Establishments.Update(editedEstablishment);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private void AttachEstablishmentType(Establishment establishment)
        {
            _context.EstablishmentTypes.Attach(establishment.EstablishmentType);
            _context.Entry(establishment.EstablishmentType).State = EntityState.Unchanged;
        }

        private void AttachCity(Establishment establishment)
        {
            _context.Cities.Attach(establishment.City);
            _context.Entry(establishment.City).State = EntityState.Unchanged;
        }
    }
}