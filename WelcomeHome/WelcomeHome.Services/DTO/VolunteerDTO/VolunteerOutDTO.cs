namespace WelcomeHome.Services.DTO
{
    public record VolunteerOutDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string SocialUrl { get; set; }

        public EstablishmentOutDTO Establishment { get; set; }

    }
}
