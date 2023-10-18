using WelcomeHome.DAL.Models;
using WelcomeHomeModels.Repositories;

namespace WelcomeHome.DAL.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private DatabaseContext _context;

        public EventsRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public IEnumerable<Event> GetEvents()
        {
            return _context.Events.ToList();
        }

        public Event? GetEventById(int id)
        {
            //return _context.Events.Find(id);
            return _context.Events.FirstOrDefault(e => e.Id == id);
        }

        //public IEnumerable<Event> GetEventsByType(int typeId)
        //{
        //    return _context.Events.Where(e => e.EventTypeId == typeId).ToList();
        //}

        public void AddEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);
        }

        public void DeleteEvent(int id)
        {
            //var existingEvent = _context.Events.Find(id);
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
            //var existingEvent = _context.Events.Find(id);
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

        //public IEnumerable<Volunteer> GetEventHosts(int id)
        //{
        //    var result = _context.Events.FirstOrDefault(e=>e.Id == id).Volunteers;
        //    return result.ToList();
        //}
    }
}