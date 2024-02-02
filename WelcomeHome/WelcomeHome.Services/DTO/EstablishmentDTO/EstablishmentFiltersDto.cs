namespace WelcomeHome.Services.DTO.EstablishmentDTO;

public record EstablishmentFiltersDto
{
    public int? EstablishmentTypeId { get; init; }

    public int? CityId { get; init; }
}
