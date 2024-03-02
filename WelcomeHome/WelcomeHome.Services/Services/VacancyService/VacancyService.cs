using AutoMapper;
using FluentValidation;
using WelcomeHome.DAL.Dto;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO.VacancyDTO;
using WelcomeHome.Services.ServiceClients.RobotaUa;
using WelcomeHome.Services.Validators;

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

    public async Task<IEnumerable<VacancyDTO>> GetAllAsync(PaginationOptionsDTO paginationOptions)
    {
        var validator = ValidatorFactory.GetValidatorByType(paginationOptions) as AbstractValidator<PaginationOptionsDTO>;
        await validator.ValidateAndThrowAsync(paginationOptions);

        var mappedPaginationOptions = _mapper.Map<PaginationOptionsDto>(paginationOptions);
        var vacanciesFromDatabase = _unitOfWork.VacancyRepository.GetAll(mappedPaginationOptions)
                                                                               .Select(v => _mapper.Map<VacancyDTO>(v))
                                                                               .ToList();

        // here change pagination options judging from how many pages is in database vacancies
        var vacanciesFromRobotaUa = await _robotaUaServiceClient.GetAllVacanciesAsync(paginationOptions)
                                                                                    .ConfigureAwait(false);
        return vacanciesFromDatabase.Union(vacanciesFromRobotaUa);
    }
}
