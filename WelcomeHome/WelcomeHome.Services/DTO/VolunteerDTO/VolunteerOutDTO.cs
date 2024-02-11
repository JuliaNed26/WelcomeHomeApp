namespace WelcomeHome.Services.DTO
{
    public record VolunteerOutDTO
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string SocialUrl { get; set; }

        public bool IsVerified { get; set; }

        public EstablishmentFullInfoDTO Establishment { get; set; }

    }
}
