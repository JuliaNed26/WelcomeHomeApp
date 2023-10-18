using WelcomeHome.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WelcomeHome.DAL.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private WelcomeHomeDbContext _context;

        public EventsRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await _context.Events.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
        }

        public async Task AddEvent(Event newEvent)
        {
            await _context.Events.AddAsync(newEvent).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteEvent(int id)
        {
            var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
            _context.Events.Remove(existingEvent);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateEvent(int id, Event editedEvent)
        {
            var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);

            existingEvent.Name = editedEvent.Name;
            existingEvent.Date = editedEvent.Date;
            existingEvent.Description = editedEvent.Description;
            existingEvent.EstablishmentId = editedEvent.EstablishmentId;
            existingEvent.EventTypeId = editedEvent.EventTypeId;
            existingEvent.VolunteerId = editedEvent.VolunteerId;

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}