using AutoMapper;
using FluentValidation;
using WelcomeHome.DAL.Dto;
using WelcomeHome.DAL.Exceptions;
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

    public async Task<VacanciesWithTotalPagesCountDto> GetAllAsync(PaginationOptionsDTO paginationOptions)
    {
        var validator = ValidatorFactory.GetValidatorByType(paginationOptions) as AbstractValidator<PaginationOptionsDTO>;
        await validator.ValidateAndThrowAsync(paginationOptions);

        IEnumerable<VacancyDTO> allVacanciesForPage = new List<VacancyDTO>();

        var mappedPaginationOptions = _mapper.Map<PaginationOptionsDto>(paginationOptions);
        var vacanciesFromDatabase = _unitOfWork.VacancyRepository.GetAll(mappedPaginationOptions)
                                                                                            .ToList();

        bool shouldGetFromDatabase = false;

        if (vacanciesFromDatabase.Count != 0 && vacanciesFromDatabase[0].TotalPagesCount <= paginationOptions.PageNumber)
        {
            allVacanciesForPage = vacanciesFromDatabase.Select(v => _mapper.Map<VacancyDTO>(v));
            shouldGetFromDatabase = true;
        }

        var vacanciesFromRobotaUa = await _robotaUaServiceClient.GetAllVacanciesAsync(paginationOptions, shouldGetFromDatabase)
                                                                                    .ConfigureAwait(false);

        if (!shouldGetFromDatabase)
        {
            allVacanciesForPage = vacanciesFromRobotaUa.Vacancies;
        }

        return new()
        {
            Vacancies = allVacanciesForPage,
            PagesCount = GetTotalPagesCount(),
        };

        int GetTotalPagesCount()
        {
            var robotaUaVacanciesPagesCount = (int)(vacanciesFromRobotaUa.TotalCount / paginationOptions.CountOnPage)
                                                       + ((vacanciesFromRobotaUa.TotalCount % paginationOptions.CountOnPage) == 0
                                                           ? 0
                                                           : 1);

            var vacanciesDatabasePagesCount = vacanciesFromDatabase.Count == 0
                                                  ? 0
                                                  : vacanciesFromDatabase[0].TotalPagesCount;

            return vacanciesDatabasePagesCount + robotaUaVacanciesPagesCount;
        }
    }

    public async Task<VacancyDTO> GetAsync(long id, bool fromRobotaUa)
    {
        if (fromRobotaUa)
        {
            return await _robotaUaServiceClient.GetAsync(id).ConfigureAwait(false);
        }

        var foundVacancy = await _unitOfWork.VacancyRepository.GetByIdAsync(id).ConfigureAwait(false)
                           ?? throw new NotFoundException($"Vacancy with id {id} was not found");

        return _mapper.Map<VacancyDTO>(foundVacancy);
    }
}
