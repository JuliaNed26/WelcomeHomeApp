using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Event
    {
        [Key]
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long? EstablishmentId { get; set; }
        public long EventTypeId { get; set; }
        public long? VolunteerId { get; set; }
        public Establishment? Establishment { get; set; }
        public EventType EventType { get; set; }
        public Volunteer? Volunteer { get; set; } = null!;
    }
}
