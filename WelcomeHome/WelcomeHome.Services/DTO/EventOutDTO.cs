using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHome.Services.DTO
{
    public record EventOutDTO
    {
        public Guid Id { get; set; }

        public DateTime? Date { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 

        public Guid EstablishmentId { get; set; }

        public Guid EventTypeId { get; set; }

        public Guid VolunteerId { get; set; }
    }
}
