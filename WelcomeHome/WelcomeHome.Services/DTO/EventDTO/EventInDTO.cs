using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.Services.DTO
{
    public record EventInDTO
    {
        public DateTime? Date { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public int EstablishmentId { get; set; }

        public int EventTypeId { get; set; }

        public int VolunteerId { get; set; }

    }
}
