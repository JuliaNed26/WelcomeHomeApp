using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeHome.DAL.Models
{
    [Table("Cities")]
    public class City
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long CountryId { get; set; }
        public Country Country { get; set; } = null!;

        public ICollection<Vacancy>? Vacancy { get; set; }
    }
}