namespace WelcomeHome.Services.DTO.VacancyDTO;

public record VacanciesWithTotalPagesCountDto
{
    public IEnumerable<VacancyDTO> Vacancies { get; internal set; }
    public int PagesCount { get; internal set; }
}