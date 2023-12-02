using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeHome.DAL.Models
{
    [Table("Cities")]
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;
    }
}