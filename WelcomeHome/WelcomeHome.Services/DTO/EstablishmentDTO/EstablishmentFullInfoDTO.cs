using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public record EstablishmentFullInfoDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string? PageURL { get; init; }
        public string? PhoneNumber { get; init; }
        public string? OtherContacts { get; init; }
        public int CityId { get; init; }
        public int EstablishmentTypeId { get; init; }
    }
}
