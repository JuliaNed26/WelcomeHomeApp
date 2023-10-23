using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Volunteer
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telegram { get; set; } = "";
        public string? Document { get; set; }
        public Guid? EstablishmentId { get; set; }
        public Guid ContractId { get; set; }
        public Establishment? Establishment { get; set; }
        public Contract? Contract { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
