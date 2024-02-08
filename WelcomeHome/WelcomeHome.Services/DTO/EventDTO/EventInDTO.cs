using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.Services.DTO.EventDto
{
    public record EventInDTO
    {
        public DateTime? Date { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public long? EstablishmentId { get; set; }

        public long? EventTypeId { get; set; }

        public long? VolunteerId { get; set; }

    }
}
