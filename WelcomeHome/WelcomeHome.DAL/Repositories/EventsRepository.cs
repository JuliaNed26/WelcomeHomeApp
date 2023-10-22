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

        public async Task<IEnumerable<Event>> GetAllAsync()
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

        public async Task Add(Event newEvent)
        {

            await _context.Events.AddAsync(newEvent).ConfigureAwait(false);
            await AttachEstablishmentAsync(newEvent).ConfigureAwait(false);
            await AttachEventTypeAsync(newEvent).ConfigureAwait(false);
            await AttachVolunteerAsync(newEvent).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(Guid id)
        {
            var existingEvent = await _context.Events.SingleAsync(e => e.Id == id).ConfigureAwait(false);
            _context.Events.Remove(existingEvent);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(Event editedEvent)
        {
            var existingEvent = await _context.Events.SingleAsync(e => e.Id == editedEvent.Id).ConfigureAwait(false);

            existingEvent.Name = editedEvent.Name;
            existingEvent.Date = editedEvent.Date;
            existingEvent.Description = editedEvent.Description;
            existingEvent.EstablishmentId = editedEvent.EstablishmentId;
            existingEvent.EventTypeId = editedEvent.EventTypeId;
            existingEvent.VolunteerId = editedEvent.VolunteerId;

            _context.Events.Update(existingEvent);

            await AttachEstablishmentAsync(editedEvent).ConfigureAwait(false);
            await AttachEventTypeAsync(editedEvent).ConfigureAwait(false);
            await AttachVolunteerAsync(editedEvent).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task AttachEstablishmentAsync(Event _event)
        {
            var existingEstablishment = await _context.Establishments.SingleAsync(e => e.Id == _event.EstablishmentId).ConfigureAwait(false);

            _context.Establishments.Attach(existingEstablishment);
            _context.Entry(existingEstablishment).State = EntityState.Unchanged;
        }

        private async Task AttachEventTypeAsync(Event _event)
        {
            var existingEventType = await _context.EventTypes.SingleAsync(et => et.Id == _event.EventTypeId).ConfigureAwait(false);

            _context.EventTypes.Attach(existingEventType);
            _context.Entry(existingEventType).State = EntityState.Unchanged;
        }
        private async Task AttachVolunteerAsync(Event _event)
        {
            var existingVolunteer = await _context.Volunteers.SingleAsync(v => v.Id == _event.VolunteerId).ConfigureAwait(false);

            _context.Volunteers.Attach(existingVolunteer);
            _context.Entry(existingVolunteer).State = EntityState.Unchanged;
        }
    }
}