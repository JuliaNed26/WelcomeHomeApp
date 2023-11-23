using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WelcomeHome.DAL.Models
{
    public class Volunteer
    {
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Telegram { get; set; } = "";
        public Guid? EstablishmentId { get; set; }
        public Establishment? Establishment { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
