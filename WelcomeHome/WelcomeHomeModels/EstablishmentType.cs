using System.ComponentModel.DataAnnotations;

namespace WelcomeHomeModels
{
    public class EstablishmentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}