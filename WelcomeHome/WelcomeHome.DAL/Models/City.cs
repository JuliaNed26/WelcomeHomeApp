using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeHomeModels.Models
{
    [Table("Cities")]
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Country Country { get; set; } = null!;
    }
}