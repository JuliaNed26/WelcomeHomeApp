using WelcomeHome.Services.DTO;

namespace WelcomeHome.Services.Services;

public interface ICityCountryService
{
    IEnumerable<CountryOutDTO> GetAllCountries();

    IEnumerable<CityOutDTO> GetAllCities();

    IEnumerable<CityOutDTO> GetAllCitiesForCountry(int countryId);
}
