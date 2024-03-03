using WelcomeHome.Services.DTO.VacancyDTO;

namespace WelcomeHome.Services.Services.VacancyService;
public interface IVacancyService
{
    public Task<VacanciesWithTotalPagesCountDto> GetAllAsync(PaginationOptionsDTO paginationOptions);

    public Task<VacancyDTO> GetAsync(long id, bool fromRobotaUa);

    public Task AddAsync(VacancyAddUpdateDTO newVacancy);
}
