using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Volunteer
    {
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string SocialUrl { get; set; }
        public int? EstablishmentId { get; set; }
        public Establishment? Establishment { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
