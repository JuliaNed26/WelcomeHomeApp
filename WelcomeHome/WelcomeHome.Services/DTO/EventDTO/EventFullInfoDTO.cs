namespace WelcomeHome.Services.DTO.EventDto
{
    public record EventFullInfoDTO
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? EstablishmentId { get; set; }

        public int EventTypeId { get; set; }

        public int? VolunteerId { get; set; }
    }
}
