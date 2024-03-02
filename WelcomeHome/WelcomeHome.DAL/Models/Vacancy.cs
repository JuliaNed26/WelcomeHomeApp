using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Vacancy
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; init; }
        public string Description { get; set; }
        public float Salary { get; set; }
        public string? PageURL { get; set; }
        public string PhoneNumber { get; set; }
        public string? OtherContacts { get; set; }
        public long CityId { get; set; }
        public City City { get; set; }
    }
}
