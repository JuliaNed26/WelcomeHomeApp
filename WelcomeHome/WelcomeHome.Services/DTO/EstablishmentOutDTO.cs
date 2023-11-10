using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public record EstablishmentOutDTO
    {
	    public Guid Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string? PageURL { get; init; }
        public string? PhoneNumber { get; init; }
        public string? OtherContacts { get; init; }
        public ICollection<Event>? Events { get; init; }
        public City City { get; init; }
        public EstablishmentType EstablishmentType { get; init; }
    }
}
