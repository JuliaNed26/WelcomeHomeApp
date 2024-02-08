namespace WelcomeHome.Services.DTO
{
    public record UserOutDTO
    {
        public long Id { get; set; }

        public string FullName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
