using System.ComponentModel.DataAnnotations;

namespace WelcomeHomeModels.Models
{
    public class UserCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}