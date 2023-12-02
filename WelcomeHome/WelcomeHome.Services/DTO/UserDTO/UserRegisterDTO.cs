using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.Services.DTO
{
    public record UserRegisterDTO
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required, Phone]
        public string PhoneNumber { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
