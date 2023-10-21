using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class SocialPayment
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Amount { get; set; }
        public string Documents { get; set; } = null!;
        public Establishment Establishment { get; set; } = null!;
        public UserCategory UserCategory { get; set; } = null!;
    }
}
