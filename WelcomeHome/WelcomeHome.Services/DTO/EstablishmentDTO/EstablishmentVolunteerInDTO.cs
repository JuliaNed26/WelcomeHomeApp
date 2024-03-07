﻿using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.Services.DTO
{
    public record EstablishmentVolunteerInDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Address { get; init; }
        public string? PageURL { get; init; }
        public string? PhoneNumber { get; init; }
        public string? OtherContacts { get; init; }
        public long CityId { get; init; }
        public long CreatorId { get; set; }
    }
}
