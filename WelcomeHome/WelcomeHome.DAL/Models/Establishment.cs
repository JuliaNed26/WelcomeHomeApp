﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeHome.DAL.Models
{
    [Table("Establishments")]
    public class Establishment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int EstablishmentTypeId { get; set; }
        public int CityId { get; set; }
        public string? PageURL { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OtherContacts { get; set; }
        public City City { get; set; } = null!;
        public EstablishmentType EstablishmentType { get; set; } = null!;
        public ICollection<Event>? Events { get; set; }

    }
}
