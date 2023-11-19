using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public record EstablishmentInDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Address { get; init; }
        public string? PageURL { get; init; }
        public string? PhoneNumber { get; init; }
        public string? OtherContacts { get; init; }
        public Guid CityId { get; init; }
        public Guid EstablishmentTypeId { get; init; }
    }
}
