using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<City>? Cities { get; set; }
    }
}