using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Volunteer
    {
        [Key]
        public long UserId { get; set; }
        public User User { get; set; }
        public string SocialUrl { get; set; }
        public bool IsVerified { get; set; }
        public long? OrganizationId { get; set; }
        public Establishment? Organization { get; set; }
        public ICollection<Event>? Events { get; set; }
        public ICollection<Establishment>? Establishments { get; set; }
    }
}
