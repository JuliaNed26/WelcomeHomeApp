namespace WelcomeHome.DAL.Dto;

public record EstablishmentRetrievalFiltersDto
{
    public int? EstablishmentTypeId { get; init; }

    public int? CityId { get; init; }
}
