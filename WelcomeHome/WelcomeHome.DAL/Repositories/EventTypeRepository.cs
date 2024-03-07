using Microsoft.EntityFrameworkCore;
using WelcomeHome.DAL.Exceptions;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private readonly WelcomeHomeDbContext _context;

        public EventTypeRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<EventType> GetAll()
        {
            return _context.EventTypes.Include(et => et.Events)
                                      .AsNoTracking()
                                      .Select(et => et);
        }

        public async Task<EventType?> GetByIdAsync(long id)
        {
            return await _context.EventTypes.Include(et => et.Events)
                                            .FirstOrDefaultAsync(e => e.Id == id)
                                            .ConfigureAwait(false);
        }

        public async Task<EventType?> GetByNameAsync(string name)
        {
            return await _context.EventTypes.Include(et => et.Events)
                                            .FirstOrDefaultAsync(e => e.Name == name)
                                            .ConfigureAwait(false);
        }

        public async Task AddAsync(EventType newEventType)
        {

            await _context.EventTypes.AddAsync(newEventType).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(long id)
        {
            var existingEventType = await _context.EventTypes
                                                  .FindAsync(id)
                                                  .ConfigureAwait(false)
                                    ?? throw new NotFoundException($"Event type with Id {id} not found for deletion.");
            _context.EventTypes.Remove(existingEventType);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(EventType editedEventType)
        {
            if (editedEventType.Id == 0)
            {
                throw new NotFoundException($"Event type with id {editedEventType.Id} was not found");
            }
            _context.EventTypes.Update(editedEventType);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
