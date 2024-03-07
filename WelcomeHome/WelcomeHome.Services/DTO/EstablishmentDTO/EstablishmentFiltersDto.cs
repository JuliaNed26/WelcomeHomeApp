namespace WelcomeHome.Services.DTO.EstablishmentDTO;

public record EstablishmentFiltersDto
{
    public long? EstablishmentTypeId { get; init; }

    public long? CityId { get; init; }
}
