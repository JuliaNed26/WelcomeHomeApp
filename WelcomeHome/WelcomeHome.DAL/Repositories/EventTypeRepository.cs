using WelcomeHome.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace WelcomeHome.DAL.Repositories
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private WelcomeHomeDbContext _context;

        public EventTypeRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<EventType>> GetAllAsync()
        {
            return _context.EventTypes.Include(et => et.Events)
                                      .AsNoTracking()
                                      .Select(et => et);
        }

        public async Task<EventType?> GetByIdAsync(Guid id)
        {
            return await _context.EventTypes.Include(et => et.Events)
                                            .FirstOrDefaultAsync(e => e.Id == id)
                                            .ConfigureAwait(false);
        }

        public async Task Add(EventType newEventType)
        {

            await _context.EventTypes.AddAsync(newEventType).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(Guid id)
        {
            var existingEventType = await _context.EventTypes.SingleAsync(et => et.Id == id).ConfigureAwait(false);
            _context.EventTypes.Remove(existingEventType);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(Event editedEventType)
        {
            var existingEventType = await _context.EventTypes.SingleAsync(et => et.Id == editedEventType.Id).ConfigureAwait(false);

            existingEventType.Name = editedEventType.Name;
            _context.EventTypes.Update(existingEventType);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
