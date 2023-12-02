using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CountryController : ControllerBase
{
    private readonly ICityCountryService _cityCountryService;

    public CountryController(ICityCountryService cityCountryService)
    {
        _cityCountryService = cityCountryService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CountryOutDTO>> GetAll()
    {
        var allCountries = _cityCountryService.GetAllCountries();
        return Ok(allCountries);
    }
}
