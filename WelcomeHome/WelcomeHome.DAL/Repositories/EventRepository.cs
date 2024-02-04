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

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.Include(e => e.Establishment)
                                        .Include(e => e.EventType)
                                        .Include(e => e.Volunteer)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(e => e.Id == id)
                                        .ConfigureAwait(false);
        }

        public IEnumerable<Event> GetByEventType(int eventTypeId)
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

            await _context.Events.AddAsync(newEvent).ConfigureAwait(false);

            AttachEventType(newEvent.EventType);
            if (newEvent.Establishment != null)
            {
                AttachEstablishment(newEvent.Establishment);
            }

            if (newEvent.Volunteer != null)
            {
                AttachVolunteer(newEvent.Volunteer);
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
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
            AttachEventType(editedEvent.EventType);
            if (editedEvent.Establishment != null)
            {
                AttachEstablishment(editedEvent.Establishment);
            }

            if (editedEvent.Volunteer != null)
            {
                AttachVolunteer(editedEvent.Volunteer);
            }
            _context.Events.Update(editedEvent);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private void AttachEstablishment(Establishment establishment)
        {
            _context.Establishments.Attach(establishment);
            _context.Entry(establishment).State = EntityState.Unchanged;
        }

        private void AttachEventType(EventType eventType)
        {
            _context.EventTypes.Attach(eventType);
            _context.Entry(eventType).State = EntityState.Unchanged;
        }
        private void AttachVolunteer(Volunteer volunteer)
        {
            _context.Volunteers.Attach(volunteer);
            _context.Entry(volunteer).State = EntityState.Unchanged;
        }
    }
}