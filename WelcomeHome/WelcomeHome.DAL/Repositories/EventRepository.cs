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
            AttachEventType(newEvent);
            AttachEstablishment(newEvent);
            AttachVolunteer(newEvent);

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
            AttachEventType(editedEvent);
            AttachEstablishment(editedEvent);
            AttachVolunteer(editedEvent);

            _context.Events.Update(editedEvent);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private void AttachEstablishment(Event eventEntity)
        {
            if (eventEntity.Establishment != null)
            {
                _context.Establishments.Attach(eventEntity.Establishment);
                _context.Entry(eventEntity.Establishment).State = EntityState.Unchanged;
            }
        }

        private void AttachEventType(Event eventEntity)
        {
            _context.EventTypes.Attach(eventEntity.EventType);
            _context.Entry(eventEntity.EventType).State = EntityState.Unchanged;
        }

        private void AttachVolunteer(Event eventEntity)
        {
            if (eventEntity.Volunteer != null)
            {
                _context.Volunteers.Attach(eventEntity.Volunteer);
                _context.Entry(eventEntity.Volunteer).State = EntityState.Unchanged;
            }
        }
    }
}