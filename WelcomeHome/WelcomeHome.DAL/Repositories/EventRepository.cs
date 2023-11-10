using WelcomeHome.DAL.Models;
using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Exceptions;

namespace WelcomeHome.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private WelcomeHomeDbContext _context;

        public EventRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events.Include(e => e.Establishment)
                                  .Include(e=>e.EventType)
                                  .Include(e=>e.Volunteer)
                                  .AsNoTracking()
                                  .Select(e => e);
        }

        public async Task<Event?> GetByIdAsync(Guid id)
        {
            return await _context.Events.Include(e => e.Establishment)
                                        .Include(e => e.EventType)
                                        .Include(e => e.Volunteer)
                                        .FirstOrDefaultAsync(e => e.Id == id)
                                        .ConfigureAwait(false);
        }

        public async Task AddAsync(Event newEvent)
        {

            await _context.Events.AddAsync(newEvent).ConfigureAwait(false);
            await AttachEstablishmentAsync(newEvent.Establishment).ConfigureAwait(false);
            await AttachEventTypeAsync(newEvent.EventType).ConfigureAwait(false);
            await AttachVolunteerAsync(newEvent.Volunteer).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id)
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
			await AttachEstablishmentAsync(editedEvent.Establishment).ConfigureAwait(false);
			await AttachEventTypeAsync(editedEvent.EventType).ConfigureAwait(false);
			await AttachVolunteerAsync(editedEvent.Volunteer).ConfigureAwait(false);
			_context.Events.Update(editedEvent);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task AttachEstablishmentAsync(Establishment establishment)
        {
            _context.Establishments.Attach(establishment);
            _context.Entry(establishment).State = EntityState.Unchanged;
        }

        private async Task AttachEventTypeAsync(EventType eventType)
        {
            _context.EventTypes.Attach(eventType);
            _context.Entry(eventType).State = EntityState.Unchanged;
        }
        private async Task AttachVolunteerAsync(Volunteer volunteer)
        {
            _context.Volunteers.Attach(volunteer);
            _context.Entry(volunteer).State = EntityState.Unchanged;
        }
    }
}