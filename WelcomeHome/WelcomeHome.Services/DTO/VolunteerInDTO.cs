using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHome.Services.DTO
{
    public record VolunteerInDTO
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telegram { get; set; }

        public string? Document { get; set; }

        public Guid EstablishmentId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
