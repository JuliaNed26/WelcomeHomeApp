using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services;

public interface ICityCountryService
{
    IEnumerable<CountryOutDto> GetAllCountries();

    IEnumerable<CityOutDto> GetAllCities();

    IEnumerable<CityOutDto> GetAllCitiesForCountry(Guid countryId);
}
