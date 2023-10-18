using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Establishment? Establishment { get; set; }
        public EventType? EventType { get; set; }
        public Volunteer Volunteer { get; set; } = null!;
    }
}
