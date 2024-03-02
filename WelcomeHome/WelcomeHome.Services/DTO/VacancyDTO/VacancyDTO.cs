namespace WelcomeHome.Services.DTO.VacancyDTO;

public record VacancyDTO
{
    public long Id { get; init; }

    public string Name { get; init; }

    public string CompanyName { get; init; }

    public string Description { get; init; }

    public float Salary { get; init; }

    public float SalaryFrom { get; init; }

    public float SalaryTo { get; init; }

    public string? PageURL { get; init; }

    public string? PhoneNumber { get; init; }

    public string? OtherContacts { get; init; }

    public long CityId { get; set; }

    public string CityName { get; set; }

    public string? MetroName { get; set; }

    public string? DistrictName { get; set; }
}
