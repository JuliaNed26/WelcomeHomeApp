﻿namespace WelcomeHome.Services.DTO.EventDto
{
    public record EventOutDTO
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public EstablishmentFullInfoDTO? Establishment { get; set; }

        public string EventTypeName { get; set; }

        public VolunteerOutDTO? Volunteer { get; set; }
    }
}
