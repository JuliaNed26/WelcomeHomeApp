namespace WelcomeHome.DAL.Dto;

public class VacancyWithTotalPagesCount
{
    public int TotalPagesCount { get; init; }
    public long Id { get; init; }
    public string Name { get; init; }
    public string CompanyName { get; init; }
    public string Description { get; init; }
    public float Salary { get; init; }
    public string? PageURL { get; init; }
    public string PhoneNumber { get; init; }
    public string? OtherContacts { get; init; }
    public long CityId { get; init; }
    public string CityName { get; init; }
}
