using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Vacancy
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Salary { get; set; }
        public Guid EstablishmentId { get; set; }
        public Establishment Establishment { get; set; } = null!;
        public string PageURL { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? OtherContacts { get; set; }

    }
}
