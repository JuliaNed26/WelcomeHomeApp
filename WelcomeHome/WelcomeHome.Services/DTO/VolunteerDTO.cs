using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHome.Services.DTO
{
    public class VolunteerDTO
    {
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Telegram { get; set; }

        public string? Document { get; set; }

        public Guid? EstablishmentId { get; set; }

        public string? EstablishmentName { get; set; }

        public string? EstablishmentCityName { get; set; }

        public string? EstablishmentPageURL { get; set; }

        public string? EstablishmentPhoneNumber { get; set; }

        public string? EstablishmentOtherContacts { get; set; }

    }
}
