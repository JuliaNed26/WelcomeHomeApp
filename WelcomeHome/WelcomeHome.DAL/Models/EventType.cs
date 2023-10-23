using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class EventType
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Event>? Events { get; set; }

    }
}