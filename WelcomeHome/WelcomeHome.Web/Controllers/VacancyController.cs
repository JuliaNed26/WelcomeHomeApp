using Microsoft.AspNetCore.Mvc;
using WelcomeHome.Services.DTO.VacancyDTO;
using WelcomeHome.Services.Services.VacancyService;

namespace WelcomeHome.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacancyController : ControllerBase
{
    private readonly IVacancyService _vacancyService;

    public VacancyController(IVacancyService vacancyService)
    {
        _vacancyService = vacancyService;
    }

    [HttpGet]
    public async Task<ActionResult<VacanciesWithTotalPagesCountDto>> GetAllAsync(
        [FromQuery] int page,
        [FromQuery] int countOnPage)
    {
        var paginationOptions = new PaginationOptionsDTO()
        {
            PageNumber = page,
            CountOnPage = countOnPage,
        };

        var allVacancies = await _vacancyService.GetAllAsync(paginationOptions).ConfigureAwait(false);
        return Ok(allVacancies);
    }

    [HttpGet("{id}/{fromRobotaUa}")]
    public async Task<ActionResult<VacancyDTO>> GetAsync(long id, bool fromRobotaUa)
    {
        var foundVacancy = await _vacancyService.GetAsync(id, fromRobotaUa).ConfigureAwait(false);
        return Ok(foundVacancy);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(VacancyAddUpdateDTO newVacancy)
    {
        await _vacancyService.AddAsync(newVacancy).ConfigureAwait(false);
        return Ok();
    }
}
