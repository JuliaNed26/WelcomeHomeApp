using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PageURL { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? OtherContacts { get; set; }
    }
}
