using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public record EstablishmentFullInfoDTO
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string? PageURL { get; init; }
        public string? PhoneNumber { get; init; }
        public string? OtherContacts { get; init; }
        public long CityId { get; init; }
        public long EstablishmentTypeId { get; init; }
        public long CreatorId { get; set; }
    }
}
