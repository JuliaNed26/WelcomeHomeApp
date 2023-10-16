using System.ComponentModel.DataAnnotations;

namespace WelcomeHomeModels
{
    public class UserCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}