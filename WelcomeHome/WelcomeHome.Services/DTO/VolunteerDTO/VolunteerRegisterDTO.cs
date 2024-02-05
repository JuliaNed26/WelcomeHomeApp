using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.Services.DTO
{
    public record VolunteerRegisterDTO
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string SocialUrl { get; set; }

        public int? EstablishmentId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
