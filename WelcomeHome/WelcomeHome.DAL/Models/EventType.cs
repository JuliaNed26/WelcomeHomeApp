using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class EventType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Event> Events { get; set; }
        public EventType()
        {
            Events = new List<Event>();
        }
    }
}