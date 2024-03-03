using WelcomeHome.Services.DTO.VacancyDTO;

namespace WelcomeHome.Services.Services.VacancyService;
public interface IVacancyService
{
    public Task<VacanciesWithTotalPagesCountDto> GetAllAsync(PaginationOptionsDTO paginationOptions);
}
