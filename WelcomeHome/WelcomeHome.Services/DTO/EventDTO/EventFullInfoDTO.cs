namespace WelcomeHome.Services.DTO.EventDto
{
    public record EventFullInfoDTO
    {
        public long Id { get; set; }

        public DateTime? Date { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long? EstablishmentId { get; set; }

        public long EventTypeId { get; set; }

        public long? VolunteerId { get; set; }
    }
}
