using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Salary { get; set; }
        public int EstablishmentId { get; set; }
        public Establishment Establishment { get; set; } = null!;
        public string PageURL { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? OtherContacts { get; set; }

    }
}
