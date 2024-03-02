﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeHome.DAL.Models
{
    [Table("Establishments")]
    public class Establishment
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long EstablishmentTypeId { get; set; }
        public long CityId { get; set; }
        public string? PageURL { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OtherContacts { get; set; }
        public long CreatorId { get; set; }
        public Volunteer Creator { get; set; }
        public City City { get; set; } = null!;
        public EstablishmentType EstablishmentType { get; set; } = null!;
        public ICollection<Event>? Events { get; set; }
        public ICollection<Volunteer> VolunteersInOrganization { get; set; }
    }
}
