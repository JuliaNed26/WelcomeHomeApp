using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly WelcomeHomeDbContext _context;

        public EventRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events.Include(e => e.Establishment)
                                  .Include(e => e.EventType)
                                  .Include(e => e.Volunteer)
                                  .AsNoTracking()
                                  .Select(e => e);
        }

        public async Task<Event?> GetByIdAsync(long id)
        {
            return await _context.Events.Include(e => e.Establishment)
                                        .Include(e => e.EventType)
                                        .Include(e => e.Volunteer)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(e => e.Id == id)
                                        .ConfigureAwait(false);
        }

        public IEnumerable<Event> GetByEventType(long eventTypeId)
        {
            return _context.Events.Include(e => e.Establishment)
                                  .Include(e => e.EventType)
                                  .Include(e => e.Volunteer)
                                  .AsNoTracking()
                                  .Where(e => e.EventTypeId == eventTypeId)
                                  .Select(e => e);
        }

        public async Task AddAsync(Event newEvent)
        {
            await AttachEventTypeAsync(newEvent.EventTypeId).ConfigureAwait(false);
            await AttachEstablishmentAsync(newEvent.EstablishmentId).ConfigureAwait(false);
            await AttachVolunteerAsync(newEvent.VolunteerId).ConfigureAwait(false);

            await _context.Events.AddAsync(newEvent).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(long id)
        {
            var existingEvent = await _context.Events
                                              .FindAsync(id)
                                              .ConfigureAwait(false)
                                ?? throw new NotFoundException($"Event with Id {id} not found for deletion.");
            _context.Events.Remove(existingEvent);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(Event editedEvent)
        {
            await AttachEventTypeAsync(editedEvent.EventTypeId).ConfigureAwait(false);
            await AttachEstablishmentAsync(editedEvent.EstablishmentId).ConfigureAwait(false);
            await AttachVolunteerAsync(editedEvent.VolunteerId).ConfigureAwait(false);

            if (editedEvent.Id == 0)
            {
                throw new NotFoundException($"Event with id {editedEvent.Id} was not found");
            }

            _context.Events.Update(editedEvent);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task AttachEstablishmentAsync(long? establishmentId)
        {
            if (establishmentId != null)
            {
                var foundEstablishment = await _context.Establishments
                                                       .FirstOrDefaultAsync(e => e.Id == establishmentId)
                                                       .ConfigureAwait(false)
                                         ?? throw new NotFoundException($"Establishment with id {establishmentId} was not found");

                _context.Establishments.Attach(foundEstablishment);
                _context.Entry(foundEstablishment).State = EntityState.Unchanged;
            }
        }

        private async Task AttachEventTypeAsync(long eventTypeId)
        {
            var foundEventType = await _context.EventTypes
                                               .FirstOrDefaultAsync(et => et.Id == eventTypeId)
                                               .ConfigureAwait(false)
                                 ?? throw new NotFoundException($"Event type with id {eventTypeId} was not found");

            _context.EventTypes.Attach(foundEventType);
            _context.Entry(foundEventType).State = EntityState.Unchanged;
        }

        private async Task AttachVolunteerAsync(long? volunteerId)
        {
            if (volunteerId != null)
            {
                var foundVolunteer = await _context.Volunteers
                                                   .FirstOrDefaultAsync(v => v.UserId == volunteerId)
                                                   .ConfigureAwait(false)
                                     ?? throw new NotFoundException($"Volunteer with id {volunteerId} was not found");

                _context.Volunteers.Attach(foundVolunteer);
                _context.Entry(foundVolunteer).State = EntityState.Unchanged;
            }
        }
    }
}