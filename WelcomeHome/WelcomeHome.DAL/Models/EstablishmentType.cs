using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class EstablishmentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Establishment>? Establishments { get; set; }
        public ICollection<Step>? Steps { get; set; }
    }
}