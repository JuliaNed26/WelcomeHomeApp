using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private WelcomeHomeDbContext _context;

        public EventsRepository(WelcomeHomeDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Event> GetEvents()
        {
            return _context.Events.ToList();
        }

        public Event? GetEventById(int id)
        {
            return _context.Events.FirstOrDefault(e => e.Id == id);
        }

        public void AddEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);
        }

        public void DeleteEvent(int id)
        {
            var existingEvent = _context.Events.FirstOrDefault(e => e.Id == id);
            if (existingEvent != null)
            {
                _context.Events.Remove(existingEvent);
            }
            else
            {
                throw new KeyNotFoundException("Event with this Id not found");
            }
        }

        public void UpdateEvent(int id, Event editedEvent)
        {
            var existingEvent = _context.Events.FirstOrDefault(e => e.Id == id);
            if (existingEvent != null)
            {
                existingEvent.Name = editedEvent.Name;
                existingEvent.Date = editedEvent.Date;
                existingEvent.Description = editedEvent.Description;
                existingEvent.EstablishmentId = editedEvent.EstablishmentId;
                existingEvent.EventTypeId = editedEvent.EventTypeId;
            }
        }
    }
}