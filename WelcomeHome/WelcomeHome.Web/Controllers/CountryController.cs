using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICityCountryService _cityCountryService;

    public CountryController(ICityCountryService cityCountryService)
    {
        _cityCountryService = cityCountryService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CountryOutDto>> GetAll()
    {
        var allCountries = _cityCountryService.GetAllCountries();
        return Ok(allCountries);
    }
}
