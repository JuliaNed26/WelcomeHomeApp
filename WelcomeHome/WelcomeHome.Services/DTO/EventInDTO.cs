using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHome.Services.DTO
{
    public class EventInDTO
    {
        public DateTime? Date { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public Guid EstablishmentId { get; set; }

        public Guid EventTypeId { get; set; }

        public Guid VolunteerId { get; set; }

    }
}
