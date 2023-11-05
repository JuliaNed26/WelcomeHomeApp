using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class UserCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<SocialPayout>? SocialPayouts { get; set; }
    }
}