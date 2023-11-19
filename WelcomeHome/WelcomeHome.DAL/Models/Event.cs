using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid EstablishmentId { get; set; }
        public Guid EventTypeId { get; set; }
        public Guid VolunteerId { get; set; }
        public Establishment Establishment { get; set; }
        public EventType EventType { get; set; }
        public Volunteer Volunteer { get; set; } = null!;
    }
}
