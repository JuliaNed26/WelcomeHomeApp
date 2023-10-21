using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeHome.DAL.Models
{
    [Table("Establishments")]
    public class Establishment
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public City City { get; set; } = null!;
        public EstablishmentType EstablishmentType { get; set; } = null!;
        public string? PageURL { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OtherContacts { get; set; }
        public List<Event> Events { get; set; }
        public Establishment()
        {
            Events = new List<Event>();
        }
    }
}
