using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHome.Services.DTO
{
    internal class EventDTO
    {
        public DateTime? Date { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public Guid? EstablishmentId { get; set; }

        public string? EstablishmentName { get; set; }

        public string? EstablishmentCityName { get; set; }

        //наприклад посилання на гугл-мапи
        public string? EstablishmentPageURL { get; set; }

        //а що буде в цій таблиці взагалі?
        public Guid EventTypeId { get; set; }

        public Guid VolunteerId { get; set; }

    }
}
