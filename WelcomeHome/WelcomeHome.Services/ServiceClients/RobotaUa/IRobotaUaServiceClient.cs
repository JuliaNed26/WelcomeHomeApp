using WelcomeHome.Services.DTO.VacancyDTO;

namespace WelcomeHome.Services.ServiceClients.RobotaUa;

public interface IRobotaUaServiceClient
{
    Task<(IEnumerable<VacancyDTO> Vacancies, long TotalCount)> GetAllVacanciesAsync(PaginationOptionsDTO paginationOptions, bool getOnlyTotalCount);
}
