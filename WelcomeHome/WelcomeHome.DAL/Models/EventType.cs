using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class EventType
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Event>? Events { get; set; }

    }
}