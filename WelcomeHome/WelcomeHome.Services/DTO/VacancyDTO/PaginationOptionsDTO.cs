namespace WelcomeHome.Services.DTO.VacancyDTO;
public record PaginationOptionsDTO
{
    public int PageNumber { get; init; }

    public int CountOnPage { get; init; }
}
