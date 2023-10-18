using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class UserCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}