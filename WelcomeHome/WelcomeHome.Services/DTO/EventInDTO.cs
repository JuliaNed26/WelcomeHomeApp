using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Guid VolunteerId { get; set; }

    }
}
