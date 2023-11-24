﻿using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO;
using WelcomeHome.Services.Services;

namespace WelcomeHome.Web.Controllers;

[Route("[controller]")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly ICityCountryService _cityCountryService;

    public CityController(ICityCountryService cityCountryService)
    {
        _cityCountryService = cityCountryService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CityOutDto>> GetAll()
    {
        var allCities = _cityCountryService.GetAllCities();
        return Ok(allCities);
    }

    [HttpGet("country/{countryId}")]
    public ActionResult<IEnumerable<CityOutDto>> GetForCountry(Guid countryId)
    {
        var cities = _cityCountryService.GetAllCitiesForCountry(countryId);
        return Ok(cities);
    }
}
