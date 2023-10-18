using WelcomeHome.DAL.Models;

namespace WelcomeHome.DAL.Repositories
{
    public interface IEventsRepository 
    {
        IEnumerable<Event> GetEvents();

        Event? GetEventById(int id);

        //IEnumerable<Event> GetEventsByType(int typeId);

        void AddEvent(Event newEvent);

        void DeleteEvent(int id);

        void UpdateEvent(int id, Event editedEvent);

        //IEnumerable<Volunteer> GetEventHosts(int id);
    }
}
