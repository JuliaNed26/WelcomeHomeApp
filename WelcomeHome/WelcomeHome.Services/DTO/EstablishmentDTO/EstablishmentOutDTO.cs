using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public record EstablishmentOutDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string? PageURL { get; init; }
        public string? PhoneNumber { get; init; }
        public string? OtherContacts { get; init; }
        public City City { get; init; }
        public EstablishmentTypeOutDTO EstablishmentType { get; init; }
    }
}
