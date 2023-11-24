using AutoMapper;
using WelcomeHome.DAL.UnitOfWork;
using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services;

public sealed class CityCountryService : ICityCountryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CityCountryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IEnumerable<CountryOutDto> GetAllCountries()
    {
        var allCountries = _unitOfWork.CountryRepository.GetAll();
        return allCountries.Select(c => _mapper.Map<CountryOutDto>(c));
    }

    public IEnumerable<CityOutDto> GetAllCities()
    {
        var allCities = _unitOfWork.CityRepository.GetAll();
        return allCities.Select(c => _mapper.Map<CityOutDto>(c));
    }

    public IEnumerable<CityOutDto> GetAllCitiesForCountry(Guid countryId)
    {
        var allCities = _unitOfWork.CityRepository.GetAll();
        return allCities
               .Where(c => c.CountryId == countryId)
               .Select(c => _mapper.Map<CityOutDto>(c));
    }
}
