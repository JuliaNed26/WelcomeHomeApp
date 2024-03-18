namespace WelcomeHome.Services.DTO.VacancyDTO;

public record VacancyAddUpdateDTO
{
    public long Id { get; set; }
    public string Name { get; init; }
    public string CompanyName { get; init; }
    public string Description { get; init; }
    public float Salary { get; init; }
    public string? PageURL { get; init; }
    public string PhoneNumber { get; init; }
    public string? OtherContacts { get; init; }
    public long CityId { get; init; }
    public long VolunteerId { get; set; }
}
