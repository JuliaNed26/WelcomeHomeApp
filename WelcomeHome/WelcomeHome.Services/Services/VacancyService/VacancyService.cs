using AutoMapper;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO.VacancyDTO;
using WelcomeHome.Services.ServiceClients.RobotaUa;

namespace WelcomeHome.Services.Services.VacancyService;

public sealed class VacancyService : IVacancyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRobotaUaServiceClient _robotaUaServiceClient;

    public VacancyService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IRobotaUaServiceClient robotaUaServiceClient)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _robotaUaServiceClient = robotaUaServiceClient;
    }

    public async Task<IEnumerable<VacancyDTO>> GetAllAsync()
    {
        var allVacancies = await _robotaUaServiceClient.GetAllVacanciesAsync().ConfigureAwait(false);
        return allVacancies;
    }
}
